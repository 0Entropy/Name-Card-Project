using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home
{
    using UnityEngine.UI;
    using strange.extensions.mediation.impl;
    public class HomeView : EventView
    {
        public Button scanBtn;
        internal enum EVENT { SCAN_BUTTON_ON_CLICK }
        internal void init()
        {
            scanBtn.onClick.AddListener(OnClickScanBtn);
        }

        public void OnClickScanBtn()
        {
            dispatcher.Dispatch(EVENT.SCAN_BUTTON_ON_CLICK);
        }
    }
}
