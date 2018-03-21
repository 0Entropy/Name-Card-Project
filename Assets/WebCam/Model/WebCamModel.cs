using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WebCam
{

    using strange.extensions.context.api;
    using strange.extensions.dispatcher.eventdispatcher.api;

    public interface IWebCamModel
    {

        IEventDispatcher dispatcher { get; set; }
        void OnPlay();
        void OnPause();
        void OnStop();
        byte[] OnCapture();
        void OnSwitch();
        //WebCamDevice[] Devices { set; get; }
        //bool HasUserAuthorization { set; get; }
        //int Index { set; get; }
        //WebCamTexture CamTexture { set; get; }
    }

    public class WebCamModel : IWebCamModel
    {
        [Inject(ContextKeys.CONTEXT_DISPATCHER)]
        public IEventDispatcher dispatcher { get; set; }

        private WebCamTexture CamTexture { set; get; }
        private int Index { set; get; }

        public WebCamModel()
        {
            CamTexture = null;
            Index = -1;
        }

        public void OnPlay()
        {
            if (CamTexture == null)
            {
                StartDevice(0);
            }
            dispatcher.Dispatch(WebCamModelEvent.CAM_TEXTURE_ON_PLAY, CamTexture);
        }

        public void OnPause()
        {
            if (CamTexture == null) return;
            CamTexture.Pause();
        }

        public void OnStop()
        {
            if (CamTexture == null) return;
            CamTexture.Stop();
        }

        public byte[] OnCapture()
        {
            if (CamTexture == null) return null;

            Texture2D photo = new Texture2D(CamTexture.width, CamTexture.height);
            photo.SetPixels(CamTexture.GetPixels());
            photo.Apply();

            //Encode to a PNG
            byte[] bytes = photo.EncodeToPNG();
            return bytes;
        }

        public void OnSwitch()
        {
            if (CamTexture == null || Index < 0) return;
            var size = WebCamTexture.devices.Length;
            if (size < 2) return;
            var index = (Index + 1) % size;

            StartDevice(index);

            dispatcher.Dispatch(WebCamModelEvent.CAM_TEXTURE_ON_PLAY, CamTexture);

        }

        private void StartDevice(int index)
        {
            Index = index;
            var name = WebCamTexture.devices[index].name;
            CamTexture = new WebCamTexture(name, 1920, 1080, 30);
            
            //CamTexture.Play();
        }

    }
}
