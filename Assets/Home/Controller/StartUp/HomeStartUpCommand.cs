using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home
{
    using strange.extensions.command.impl;
    using strange.extensions.mediation.api;
    public class HomeStartUpCommand : EventCommand
    {
        [Inject]
        public IMediationBinder mediationBinder { set; get; }

        public override void Execute()
        {
            Debug.Log("Home StartUp!!!");
            
            mediationBinder.Bind<HomeView>().To<HomeMediator>();
            mediationBinder.Bind<NavigationView>().To<NavigationMediator>();
            
        }

    }
}
