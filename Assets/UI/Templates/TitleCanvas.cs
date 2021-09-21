using Achievements;
using Basics;
using Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Templates
{
    public class TitleCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        public Canvas Canvas { get { return canvas; } }

        private void Awake()
        {
            this.Canvas.worldCamera = GameCamera.Instance.Camera;
        }
    }
}
