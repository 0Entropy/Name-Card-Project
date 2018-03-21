using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Info
{
    using strange.extensions.command.impl;
    using strange.extensions.mediation.api;

    public class InfoStartUpCommand : EventCommand
    {

        [Inject]
        public IMediationBinder mediationBinder { set; get; }
        public override void Execute()
        {
            mediationBinder.Bind<InfoView>().
                To<InfoMediator>();
        }
    }
}
