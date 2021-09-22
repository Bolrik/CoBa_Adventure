using Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Templates;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class AchievementMenuButton : GameButton
    {
        [SerializeField] private TitleCanvas titleCanvas;
        public TitleCanvas TitleCanvas { get { return titleCanvas; } }

        public override void OnClick()
        {
            this.TitleCanvas.ShowAchievementOverlay(true);
        }
    }
}