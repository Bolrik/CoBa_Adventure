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


        [SerializeField] private AchievementOverlayPage achievementOverlay;
        public AchievementOverlayPage AchievementOverlay { get { return achievementOverlay; } }

        [SerializeField] private CanvasGroup layoutPanel;
        public CanvasGroup LayoutPanel { get { return layoutPanel; } }

        bool IsAchievementOverlay { get; set; }


        private void Awake()
        {
            this.Canvas.worldCamera = GameCamera.Instance.Camera;

            this.ShowAchievementOverlay(false);
        }

        public void ShowAchievementOverlay(bool show)
        {
            this.IsAchievementOverlay = show;
            this.AchievementOverlay.Show(show);
            this.LayoutPanel.interactable = !show;
            this.LayoutPanel.blocksRaycasts = !show;
        }

        public void Update()
        {
            if (this.IsAchievementOverlay && 
                (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonUp(1)))
            {
                this.ShowAchievementOverlay(false);
            }
        }
    }
}
