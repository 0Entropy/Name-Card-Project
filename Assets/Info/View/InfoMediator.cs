using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Info
{
    using strange.extensions.mediation.impl;

    public class InfoMediator : EventMediator
    {

        [Inject]
        public InfoView view { set; get; }

        public override void OnRegister()
        {
            view.init();
            view.dispatcher.AddListener(InfoView.EVENT.BACK_TO_WEBCAM, HandleOnClickBackButton);
        }

        public override void OnRemove()
        {
            view.dispatcher.RemoveListener(InfoView.EVENT.BACK_TO_WEBCAM, HandleOnClickBackButton);

        }

        private void HandleOnClickBackButton()
        {
            dispatcher.Dispatch(Home.HomeViewEvent.NAVI_TO_WEB_CAM);
        }
    }
}