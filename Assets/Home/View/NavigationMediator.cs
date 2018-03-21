using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home
{
    using strange.extensions.mediation.impl;
    using strange.extensions.dispatcher.eventdispatcher.api;
    public class NavigationMediator : EventMediator
    {

        [Inject]
        public NavigationView view { set; get; }

        public override void OnRegister()
        {
            view.init();

            dispatcher.AddListener(HomeViewEvent.NAVI_TO_WEB_CAM, HandleOnNavigateToWenCam);
            dispatcher.AddListener(WebCam.WebCamScreenEvent.BACK_HOME, HandleOnBackHome);
            dispatcher.AddListener(WebCam.CardInfoEvent.CARD_INFO_ON_INIT, HandleOnUpdateInfoValue);
            dispatcher.AddListener(WebCam.WebCamScreenEvent.NAVI_TO_INFO_VIEW, HandleOnNavigateToInfo);
        }

        public override void OnRemove()
        {
            dispatcher.RemoveListener(HomeViewEvent.NAVI_TO_WEB_CAM, HandleOnNavigateToWenCam);
            dispatcher.RemoveListener(WebCam.WebCamScreenEvent.BACK_HOME, HandleOnBackHome);
            dispatcher.RemoveListener(WebCam.CardInfoEvent.CARD_INFO_ON_INIT, HandleOnUpdateInfoValue);
            dispatcher.RemoveListener(WebCam.WebCamScreenEvent.NAVI_TO_INFO_VIEW, HandleOnNavigateToInfo);

        }

        private void HandleOnNavigateToWenCam()
        {
            view.ShowWebCam();
        }

        private void HandleOnBackHome()
        {
            view.BackHome();
        }

        private void HandleOnNavigateToInfo()
        {
            view.ShowInfoView();
        }

        private void HandleOnUpdateInfoValue(IEvent evt)
        {
            var infos = evt.data as List<WebCam.ItemValuePair>;
            view.NavigateToInfo(infos);
        }

    }
}
