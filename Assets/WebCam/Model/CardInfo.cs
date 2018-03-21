using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WebCam
{
    using strange.extensions.context.api;
    using strange.extensions.dispatcher.eventdispatcher.api;
    public interface ICardInfo
    {
        IEventDispatcher dispatcher { get; set; }
        //List<ItemValuePair> Lookup { set; get; }
        void OnInit(List<ItemValuePair> lookup);
    }
    public class CardInfo : ICardInfo
    {
        [Inject(ContextKeys.CONTEXT_DISPATCHER)]
        public IEventDispatcher dispatcher { get; set; }

        private List<ItemValuePair> Lookup { set; get; }
        
        public void OnInit(List<ItemValuePair> lookup)
        {
            Lookup = new List<ItemValuePair>();
            foreach(var pair in lookup)
            {
                var clone = new ItemValuePair() { item = string.Copy(pair.item), value = string.Copy(pair.value) };
            }
            dispatcher.Dispatch(CardInfoEvent.CARD_INFO_ON_INIT, lookup);
        }

    }
}
