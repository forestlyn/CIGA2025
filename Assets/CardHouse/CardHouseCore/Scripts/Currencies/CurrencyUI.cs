using UnityEngine;
using UnityEngine.UI;

namespace CardHouse
{
    public class CurrencyUI : MonoBehaviour
    {
        public Image Image;
        public Text Text;
        public void Apply(CurrencyContainer resource)
        {
            Image.sprite = resource.CurrencyType.Sprite;
            var text = resource.CurrencyType.Name + ":";
            if (resource.HasMax)
            {
                if (resource.ShowReverse)
                    text += (resource.Max - resource.Amount).ToString() + "/" + resource.Max.ToString();
                else
                    text += resource.Amount.ToString() + "/" + resource.Max.ToString();
            }

            Text.text = text;
        }
    }
}
