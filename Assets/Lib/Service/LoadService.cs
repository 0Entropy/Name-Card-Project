using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    using strange.extensions.dispatcher.eventdispatcher.api;
    using strange.extensions.context.api;
    using strange.extensions.command.impl;

    public interface ILoadService
    {

        IEventDispatcher dispatcher { get; set; }
        
        void Load<T>(string url);
        
    }
    
    public class LoadService : ILoadService
    {

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contentRoot { get; set; }

        [Inject]
        public IEventDispatcher dispatcher { get; set; }

        private string url;
        private System.Type type;
        
        public void Load<T>(string url)
        {
            this.url = url;
            this.type = typeof(T);
            MonoBehaviour root = contentRoot.GetComponent<RootContextView>();
            root.StartCoroutine(Load());
        }

        IEnumerator Load()
        {

            using (WWW www = new WWW(url))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    yield return null;
                    dispatcher.Dispatch(ServiceEvent.FAILURE_TO_LOAD);
                }

                if (type.Equals(typeof(Texture2D)))
                {
                    var tex = www.texture;
                    dispatcher.Dispatch(ServiceEvent.SUCCESS_TO_LOAD, tex);
                }
                else if (type.Equals(typeof(string)))
                {
                    var text = www.text;
                    dispatcher.Dispatch(ServiceEvent.SUCCESS_TO_LOAD, text);
                }

            }

            yield return null;
        }

    }
