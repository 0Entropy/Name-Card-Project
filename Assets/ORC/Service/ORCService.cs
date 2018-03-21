using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OCR
{

    using strange.extensions.dispatcher.eventdispatcher.api;
    using strange.extensions.context.api;

    public interface IOCRService
    {
        IEventDispatcher dispatcher { get; set; }
        void Request(string url);
    }
    public class OCRService : IOCRService
    {


        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contentRoot { get; set; }

        [Inject]
        public IEventDispatcher dispatcher { get; set; }

        private string imgURL;
        private MonoBehaviour rootView;

        public void Request(string url)
        {
            imgURL = url;
            rootView = contentRoot.GetComponent<RootContextView>();
            RequestOrcAccess();
        }

        void RequestOrcAccess()
        {
            var url = URL.REQUEST_ORC_ACCESS_URL;
            WWWForm form = new WWWForm();
            form.AddField("image", imgURL);
            rootView.StartCoroutine(HandleOnRequestOrcAccess(url, form));
        }

        IEnumerator HandleOnRequestOrcAccess(string url, WWWForm form)
        {
            using (WWW www = new WWW(url, form))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {

                    dispatcher.Dispatch(OCRServiceEvent.FAILURE_TO_RECO);
                    yield return null;
                }

                Read(www.text);
            }
            yield return null;
        }

        private void Read(string args)
        {
            //var template = new { success = 0, message = string.Empty, data = new { list = new List<WebCam.ItemValuePair>() } };
            //var reader = new JsonFx.Json.JsonReader();
            //var output = reader.Read<tArgs>(args);
            var output = JsonFx.Json.JsonReader.Deserialize<tArgs>(args);

            if (output.success != 20)
                dispatcher.Dispatch(OCRServiceEvent.FAILURE_TO_RECO);
            else
                dispatcher.Dispatch(OCRServiceEvent.SUCCESS_TO_RECO, output.data.list);


        }

        class tArgs
        {
           
            public int success { set; get; }
            public string message { set; get; }
            public tList data { set; get; }
        }

        class tList
        {
            public List<WebCam.ItemValuePair> list { set; get; }
        }


    }


}
