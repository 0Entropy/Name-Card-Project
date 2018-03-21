using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WebCam
{
    using strange.extensions.mediation.impl;
    using strange.extensions.dispatcher.eventdispatcher.api;

    public class WebCamScreenMediator : EventMediator
    {

        [Inject]
        public WebCamScreenView view { set; get; }

        public override void OnRegister()
        {
            view.init();
            view.dispatcher.AddListener(WebCamScreenView.EVENT.ON_ENABLE, HandleOnEnable);
            view.dispatcher.AddListener(WebCamScreenView.EVENT.CAPTURE_BUTTON_ON_CLICK, HandleOnClickCaptureButton);
            view.dispatcher.AddListener(WebCamScreenView.EVENT.BACK_BUTTON_ON_CLICK, HandleOnClickBackButton);
            view.dispatcher.AddListener(WebCamScreenView.EVENT.SWITCH_BUTTON_ON_CLICK, HandleOnClickSwitchButton);

            dispatcher.AddListener(WebCamModelEvent.CAM_TEXTURE_ON_PLAY, HandleOnShowCamTexture);
        }

        public override void OnRemove()
        {
            view.dispatcher.RemoveListener(WebCamScreenView.EVENT.ON_ENABLE, HandleOnEnable);
            view.dispatcher.RemoveListener(WebCamScreenView.EVENT.CAPTURE_BUTTON_ON_CLICK, HandleOnClickCaptureButton);
            view.dispatcher.RemoveListener(WebCamScreenView.EVENT.BACK_BUTTON_ON_CLICK, HandleOnClickBackButton);
            view.dispatcher.RemoveListener(WebCamScreenView.EVENT.SWITCH_BUTTON_ON_CLICK, HandleOnClickSwitchButton);

            dispatcher.RemoveListener(WebCamModelEvent.CAM_TEXTURE_ON_PLAY, HandleOnShowCamTexture);
        }

        private void HandleOnEnable()
        {
            dispatcher.Dispatch(WebCamScreenEvent.PLAY_WEB_CAM);
        }

        private void HandleOnClickCaptureButton()
        {
            dispatcher.Dispatch(WebCamScreenEvent.CAPTURE_FRAME);
            dispatcher.Dispatch(WebCamScreenEvent.NAVI_TO_INFO_VIEW);
        }

        private void HandleOnClickBackButton()
        {
            dispatcher.Dispatch(WebCamScreenEvent.STOP_WEB_CAM);
            dispatcher.Dispatch(WebCamScreenEvent.BACK_HOME);
        }

        private void HandleOnClickSwitchButton()
        {
            dispatcher.Dispatch(WebCamScreenEvent.SWITCH_DEVICE);
        }

        private void HandleOnShowCamTexture(IEvent evt)
        {
            var camTex = evt.data as WebCamTexture;
            Debug.Log("Screen Mediator : " + camTex.deviceName);
            view.OnRender(camTex);
        }
    }
}
