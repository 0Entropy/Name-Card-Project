using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Info
{
    public class ItemView : MonoBehaviour
    {

        public Text itemText;
        public Text valueText;

        public void Show(string item, string value)
        {
            itemText.text = item;
            valueText.text = value;
        }

    }
}
