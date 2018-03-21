using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WebCam
{
    using strange.extensions.context.api;
    using strange.extensions.command.impl;
    public class PlayWebCamCommand : EventCommand
    {

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contentRoot { get; set; }

        [Inject]
        public IWebCamModel camModel { set; get; }

        private MonoBehaviour rootView;

        public override void Execute()
        {
            Retain();

            rootView = contentRoot.GetComponent<RootContextView>();
            
            RequestUserAuthorization();

            Debug.Log("Play Web Cam");
        }

        private void RequestUserAuthorization()
        {
            rootView.StartCoroutine(HandleOnRequestUserAuthorization());
        }

        private IEnumerator HandleOnRequestUserAuthorization()
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            //| UserAuthorization.Microphone);
            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            // | UserAuthorization.Microphone))
            {
                camModel.OnPlay();
            }
            else
            {
                dispatcher.Dispatch(WebCamScreenEvent.BACK_HOME);                
            }
            Release();
            yield return null;
        }

    }
}
