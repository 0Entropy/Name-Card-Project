using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.context.api;
using strange.extensions.command.impl;

public interface IPostService
{
    IEventDispatcher dispatcher { get; set; }

    void Request(string url, WWWForm form);
}

public class PostService : IPostService
{

    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contentRoot { get; set; }

    [Inject]
    public IEventDispatcher dispatcher { get; set; }

    private string url;
    private WWWForm form;

    public void Request(string url, WWWForm form)
    {
        this.url = url;
        this.form = form;

        MonoBehaviour root = contentRoot.GetComponent<RootContextView>();
        root.StartCoroutine(Post());
    }

    IEnumerator Post()
    {
        using (WWW www = new WWW(url, form))
        {
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                yield return null;
                dispatcher.Dispatch(ServiceEvent.FAILURE_TO_POST);
            }

            var text = www.text;
            dispatcher.Dispatch(ServiceEvent.SUCCESS_TO_POST, text);


        }
        yield return null;
    }


}
