using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace OCR
{
    public class UnitTestCommand : EventCommand
    {

        [Inject]
        public IOSSService ossService { set; get; }

        public override void Execute()
        {
            Retain();
            ossService.dispatcher.AddListener(OSSServiceEvent.SUCCESS_TO_UPLOAD, OnSuccess);
            ossService.dispatcher.AddListener(OSSServiceEvent.FAILURE_TO_UPLOAD, OnFailed);

            var tex = Resources.Load<Texture2D>("V_out");
            var bytes = tex.EncodeToJPG();
            ossService.Request("_V_out.jpg", bytes);
        }

        private void OnSuccess(IEvent evt)
        {
            ossService.dispatcher.RemoveListener(OSSServiceEvent.SUCCESS_TO_UPLOAD, OnSuccess);
            ossService.dispatcher.RemoveListener(OSSServiceEvent.FAILURE_TO_UPLOAD, OnFailed);
            var url = evt.data as string;
            Debug.Log(url);

            Release();
        }

        private void OnFailed()
        {
            ossService.dispatcher.RemoveListener(OSSServiceEvent.SUCCESS_TO_UPLOAD, OnSuccess);
            ossService.dispatcher.RemoveListener(OSSServiceEvent.FAILURE_TO_UPLOAD, OnFailed);
            Release();
        }
    }
}
