using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WebCam
{
    using strange.extensions.mediation.impl;
    using UnityEngine.UI;

    public class WebCamScreenView : EventView
    {

        public enum EVENT { ON_ENABLE, CAPTURE_BUTTON_ON_CLICK, BACK_BUTTON_ON_CLICK, SWITCH_BUTTON_ON_CLICK }

        public RawImage rawImage;
        public Button captureBtn;
        public Button backBtn;
        public Button switchBtn;

        private Quaternion baseRation;

        internal void init()
        {
            

            captureBtn.onClick.AddListener(OnClickCaptureBtn);
            backBtn.onClick.AddListener(OnClickBackBtn);
            switchBtn.onClick.AddListener(OnClickSwitchBtn);
            
            if (WebCamTexture.devices.Length < 2)
                switchBtn.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            dispatcher.Dispatch(EVENT.ON_ENABLE);
            
            //Render();
        }

        private void OnClickCaptureBtn()
        {
            dispatcher.Dispatch(EVENT.CAPTURE_BUTTON_ON_CLICK);
        }

        private void OnClickBackBtn()
        {
            dispatcher.Dispatch(EVENT.BACK_BUTTON_ON_CLICK);
        }

        private void OnClickSwitchBtn()
        {
            dispatcher.Dispatch(EVENT.SWITCH_BUTTON_ON_CLICK);
        }

        private WebCamTexture camTexture;
        internal void OnRender(WebCamTexture camTex)
        {
            if (camTex == null) return;
            camTexture = camTex;

            camTex.Play();

            var camW = camTex.width; var camH = camTex.height;
            var scrW = Screen.width; var scrH = Screen.height;

            float scaX = (float)scrW / (float)camW; float scaY = (float)scrH / (float)camH;
            float scale = Mathf.Max(scaX, scaY);
            float dstW = camW * scale; float dstH = camH * scale;

            rawImage.transform.rotation = baseRation * Quaternion.AngleAxis(camTex.videoRotationAngle, Vector3.up);

            rawImage.rectTransform.sizeDelta = new Vector2(dstW, dstH);
            rawImage.texture = camTex;
            rawImage.material.mainTexture = camTex;
        }

        private void OnGUI()
        {
            if (camTexture == null) return;
            GUI.skin.label.fontSize = 32;
            GUI.Label(new Rect(64, 64, 256, 48), camTexture.width + ", " + camTexture.height );
            GUI.Label(new Rect(64, 112, 256, 48), Screen.width + ", " + Screen.height);
            GUI.Label(new Rect(64, 160, 256, 48), rawImage.rectTransform.sizeDelta.x + ", " + rawImage.rectTransform.sizeDelta.y);
        }

    }
}
