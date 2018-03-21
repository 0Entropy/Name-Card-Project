using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WebCam
{
    using strange.extensions.command.impl;
    public class StopWebCamCommand : EventCommand
    {

        [Inject]
        public IWebCamModel camModel { set; get; }

        public override void Execute()
        {
            Debug.Log("Stop Web Cam");
        }

    }
}
