using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WebCam
{
    using strange.extensions.command.impl;
    using strange.extensions.mediation.api;
    public class WebCamStartUpCommand : EventCommand
    {
        [Inject]
        public IMediationBinder mediationBinder { set; get; }

        public override void Execute()
        {
            injectionBinder.Bind<ICardInfo>().
                To<CardInfo>().
                ToSingleton();
            injectionBinder.Bind<IWebCamModel>().
                To<WebCamModel>().
                ToSingleton();

            mediationBinder.Bind<WebCamScreenView>().
                To<WebCamScreenMediator>();

            commandBinder.Bind(WebCamScreenEvent.PLAY_WEB_CAM).
                To<PlayWebCamCommand>();
            commandBinder.Bind(WebCamScreenEvent.STOP_WEB_CAM).
                 To<StopWebCamCommand>();
            commandBinder.Bind(WebCamScreenEvent.CAPTURE_FRAME).
                 To<CaptureFrameCommand>();
            commandBinder.Bind(WebCamScreenEvent.SWITCH_DEVICE).
                 To<SwitchDeviceCommand>();
        }
    }
}
