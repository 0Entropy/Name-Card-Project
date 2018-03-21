using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Info
{
    using strange.extensions.mediation.impl;

    public class InfoView : EventView
    {
        public enum EVENT { BACK_TO_WEBCAM }

        public ItemView itemPrefab;

        public GameObject layoutPanel;
        public Button backButton;

        private List<ItemView> itemViews;

        internal void init()
        {
            backButton.onClick.AddListener(HandleOnClickBackButton);
            
            itemPrefab.CreatePool();
        }

        private void HandleOnClickBackButton()
        {
            dispatcher.Dispatch(EVENT.BACK_TO_WEBCAM);
        }

        public void Show(List<WebCam.ItemValuePair> pairs)
        {
            itemViews = new List<ItemView>();
            foreach (var pair in pairs)
            {
                var view = itemPrefab.Spawn();
                view.transform.SetParent(layoutPanel.transform);
                view.Show(pair.item, pair.value);
                itemViews.Add(view);
            }
        }

        private void OnDisable()
        {
            if (itemViews == null || itemViews.Count <= 0)
                return;

            foreach(var view in itemViews)
            {
                view.Recycle();
            }
            itemViews.Clear();
        }

    }
}
