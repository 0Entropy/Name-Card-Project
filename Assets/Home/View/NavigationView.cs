using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home
{
    using strange.extensions.mediation.impl;
    public class NavigationView : EventView
    {
        
        public GameObject homePanel;
        public GameObject webCamPanel;
        public GameObject infoPanel;

        internal void init()
        {

        }

        internal void ShowWebCam()
        {
            homePanel.SetActive(false);
            webCamPanel.SetActive(true);
            infoPanel.SetActive(false);
        }

        internal void ShowInfoView()
        {
            homePanel.SetActive(false);
            webCamPanel.SetActive(false);
            infoPanel.SetActive(true);
        }

        internal void BackHome()
        {

            homePanel.SetActive(true);
            webCamPanel.SetActive(false);
            infoPanel.SetActive(false);
        }

        internal void NavigateToInfo(List<WebCam.ItemValuePair> pairs)
        {
            ShowInfoView();
            var infoView = infoPanel.GetComponent<Info.InfoView>();
            if (infoView)
                infoView.Show(pairs);
        }
        
    }
}
