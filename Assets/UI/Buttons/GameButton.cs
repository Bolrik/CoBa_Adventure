using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Buttons
{
    public abstract class GameButton : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private Button button;
        Button Button { get { return button; } }

        [SerializeField] private Text text;
        Text Text { get { return text; } }


        public abstract void OnClick();

        public void SetText(string text)
        {
            this.Text.text = text;
        }



        public void OnPointerEnter(PointerEventData eventData)
        {
            Audio.AudioManager.Instance.Play(Audio.AudioAssets.ButtonHover);
        }
    }
}