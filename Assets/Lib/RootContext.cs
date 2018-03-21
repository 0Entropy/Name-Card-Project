
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;

public class RootContext : MVCSContext
{

    public RootContext(MonoBehaviour view) : base(view)
    {
    }

    public RootContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void mapBindings()
    {

        injectionBinder.Bind<IPostService>().To<PostService>();
        injectionBinder.Bind<ILoadService>().To<LoadService>();

        commandBinder.Bind(ContextEvent.START).
            To<RootStartUpCommand>().
            To<Home.HomeStartUpCommand>().
            To<OCR.ORCStartUpCommand>().
            To<WebCam.WebCamStartUpCommand>().
            To<Info.InfoStartUpCommand>().
            Once();

    }
}
