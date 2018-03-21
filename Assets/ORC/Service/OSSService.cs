using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.context.api;
using System.Collections;
using System.Collections.Generic;

namespace OCR
{
    public interface IOSSService
    {
        IEventDispatcher dispatcher { get; set; }
        void Request(string name, byte[] bytes);
    }

    class OSSService : IOSSService
    {

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contentRoot { get; set; }

        [Inject]
        public IEventDispatcher dispatcher { get; set; }

        private string name;
        private byte[] bytes;

        private OSS_Access_Info accessInfo;
        private MonoBehaviour rootView;

        public void Request(string name, byte[] bytes)//string url, WWWForm form)
        {
            this.name = name;
            this.bytes = bytes;

            rootView = contentRoot.GetComponent<RootContextView>();

            RequestOssAccess();
        }

        void RequestOssAccess()
        {
            var url = URL.REQUEST_OSS_ACCESS_URL;
            WWWForm form = new WWWForm();
            form.AddField("type", "EDITOR_IMAGE");
            rootView.StartCoroutine(HandleOnRequestOssAccess(url, form));
        }

        IEnumerator HandleOnRequestOssAccess(string url, WWWForm form)
        {
            using (WWW www = new WWW(url, form))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {

                    dispatcher.Dispatch(OSSServiceEvent.FAILURE_TO_UPLOAD);
                    yield return null;
                }
                
                Read(www.text);
            }
            yield return null;

        }

        private void Read(string args)
        {
            //var template = new { success = 0, message = string.Empty, data = new OSS_Access_Info() };
            //var reader = new JsonFx.Json.JsonReader();
            //var output = reader.Read(args, template);
            //var output = reader.Read<tArgs>(args);
            var output = JsonFx.Json.JsonReader.Deserialize<tArgs>(args);

            if (output.success != 20)
                dispatcher.Dispatch(OSSServiceEvent.FAILURE_TO_UPLOAD);
            else
                rootView.StartCoroutine(HandleOnUploadToOSS(output.data, name, bytes));

        }

        class tArgs
        {
            public int success { set; get; }
            public string message { set; get; }
            public OSS_Access_Info data { set; get; }
        }

        private IEnumerator HandleOnUploadToOSS(OSS_Access_Info accessInfo, string imgName, byte[] imgBytes)
        {
            Utility.HttpUtils http = new Utility.HttpUtils();
            string boundary = http.boundary;
            string name = http.boundary + imgName;
            http.SetFieldValue("name", name);
            http.SetFieldValue("key", accessInfo.dir + name);
            http.SetFieldValue("host", accessInfo.host);
            http.SetFieldValue("policy", accessInfo.policy);
            http.SetFieldValue("OSSAccessKeyId", accessInfo.accessid);
            http.SetFieldValue("success_action_status", "200");
            http.SetFieldValue("Signature", accessInfo.signature);
            http.SetFieldValue("file", name, "image/jpeg", imgBytes);
            byte[] bytes = http.MergeContent();

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
            using (WWW www = new WWW(accessInfo.host, bytes, headers))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    Debug.LogError("网络错误" + www.error);
                    dispatcher.Dispatch(OSSServiceEvent.FAILURE_TO_UPLOAD);
                    yield return null;
                }
                bytes = null;
            }
            string storedURL = accessInfo.host + accessInfo.dir + boundary + imgName;
            dispatcher.Dispatch(OSSServiceEvent.SUCCESS_TO_UPLOAD, storedURL);
            yield return null;
        }

        #region Unit Test
        public const string args = "{\"success\" : 20,\"message\": \"操作成功\", " +
            "\"data\": {\"accessid\": \"LTAIiF6eftlz5UAU\"," +
        "\"policy\": \"eyJleHBpcmF0aW9uIjoiMjAxNy0wNC0yMVQwNTo1MDo0Ni43MDhaIiwiY29uZGl0aW9ucyI6W1siY29udGVudC1sZW5ndGgtcmFuZ2UiLDAsMTA0ODU3NjAwMF0sWyJzdGFydHMtd2l0aCIsIiRrZXkiLCJvc3MvIl1dfQ==\"," +
        "\"signature\": \"FeoIZBidDshdIBdkIIMJ/47ojaI=\"," +
        "\"dir\": \"oss/\"," +
        "\"host\": \"http://yunchuang1.oss-cn-shenzhen.aliyuncs.com/\"," +
        "\"expire\": \"1492753846\"}}";
        #endregion
    }
}
