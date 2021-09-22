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
    public class AchievementOverlayPanel : MonoBehaviour
    {
        [SerializeField] private Image imageElement;
        public Image ImageElement { get { return imageElement; } }

        [SerializeField] private Text titleElement;
        public Text TitleElement { get { return titleElement; } }

        [SerializeField] private Text descElement;
        public Text DescElement { get { return descElement; } }

        [SerializeField] private Image backgroundElement;
        public Image BackgroundElement { get { return backgroundElement; } }


        AchievementData Achievement { get; set; }

        public void SetAchievement(AchievementData achievement, bool completed)
        {
            this.Achievement = achievement;

            this.ImageElement.sprite = this.Achievement.Image;
            this.TitleElement.text = this.Achievement.Title;
            this.DescElement.text = this.Achievement.Desc;

            if (completed)
            {
                Color color = Color.green;
                color.a = .45f;
                BackgroundElement.color = color;
            }
        }
    }
}
