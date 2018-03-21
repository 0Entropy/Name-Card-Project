using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OCR
{
    using strange.extensions.command.impl;
    using strange.extensions.mediation.api;
    public class ORCStartUpCommand : EventCommand
    {
        [Inject]
        public IMediationBinder mediationBinder { set; get; }

        public override void Execute()
        {
            Debug.Log("ORC StartUp!!!");

            

            injectionBinder.Bind<IOSSService>().To<OSSService>();
            injectionBinder.Bind<IOCRService>().To<OCRService>();
        }

    }
}
