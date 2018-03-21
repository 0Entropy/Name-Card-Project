using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WebCam
{
    using OCR;
    using strange.extensions.command.impl;
    using strange.extensions.dispatcher.eventdispatcher.api;
    public class CaptureFrameCommand : EventCommand
    {

        [Inject]
        public IWebCamModel camModel { set; get; }

        [Inject]
        public IOSSService ossService { set; get; }

        [Inject]
        public IOCRService orcService { set; get; }

        [Inject]
        public ICardInfo cardInfo { set; get; }

        public override void Execute()
        {
            Retain();
            Debug.Log("Capture Frame");
            ossService.dispatcher.AddListener(OSSServiceEvent.SUCCESS_TO_UPLOAD, OnSuccessUploadToOSS);
            ossService.dispatcher.AddListener(OSSServiceEvent.FAILURE_TO_UPLOAD, OnFailed);
            
            ossService.Request("_WebCamTexture.jpg", camModel.OnCapture());
        }

        private void OnSuccessUploadToOSS(IEvent evt)
        {
            ossService.dispatcher.RemoveListener(OSSServiceEvent.SUCCESS_TO_UPLOAD, OnSuccessUploadToOSS);
            ossService.dispatcher.RemoveListener(OSSServiceEvent.FAILURE_TO_UPLOAD, OnFailed);
            var url = evt.data as string;
            Debug.Log(url);

            orcService.dispatcher.AddListener(OCRServiceEvent.SUCCESS_TO_RECO, OnSuccessToOCR);
            orcService.dispatcher.AddListener(OCRServiceEvent.FAILURE_TO_RECO, OnFailed);

            orcService.Request(url);
        }

        private void OnSuccessToOCR(IEvent evt)
        {
            orcService.dispatcher.RemoveListener(OCRServiceEvent.SUCCESS_TO_RECO, OnSuccessToOCR);
            orcService.dispatcher.RemoveListener(OCRServiceEvent.FAILURE_TO_RECO, OnFailed);

            var pairs = evt.data as List<ItemValuePair>;
            cardInfo.OnInit(pairs);
            camModel.OnPause();
            Release();
        }

        private void OnFailed()
        {
            ossService.dispatcher.RemoveListener(OSSServiceEvent.SUCCESS_TO_UPLOAD, OnSuccessUploadToOSS);
            ossService.dispatcher.RemoveListener(OSSServiceEvent.FAILURE_TO_UPLOAD, OnFailed);

            orcService.dispatcher.RemoveListener(OCRServiceEvent.SUCCESS_TO_RECO, OnSuccessToOCR);
            orcService.dispatcher.RemoveListener(OCRServiceEvent.FAILURE_TO_RECO, OnFailed);

            Release();
        }

    }
}
