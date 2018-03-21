using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home
{
    using strange.extensions.mediation.impl;

    public class HomeMediator : EventMediator
    {
        [Inject]
        public HomeView view { set; get; }

        public override void OnRegister()
        {
            view.init();

            view.dispatcher.AddListener(HomeView.EVENT.SCAN_BUTTON_ON_CLICK, HandleOnButtonOnClick);
        }

        public override void OnRemove()
        {
            view.dispatcher.RemoveListener(HomeView.EVENT.SCAN_BUTTON_ON_CLICK, HandleOnButtonOnClick);
        }

        private void HandleOnButtonOnClick()
        {
            dispatcher.Dispatch(HomeViewEvent.NAVI_TO_WEB_CAM);
        }
    }
}
