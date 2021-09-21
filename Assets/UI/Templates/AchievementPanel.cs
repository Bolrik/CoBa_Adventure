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
    public class AchievementPanel : MonoBehaviour
    {
        [SerializeField] private Image imageElement;
        public Image ImageElement { get { return imageElement; } }

        [SerializeField] private Text titleElement;
        public Text TitleElement { get { return titleElement; } }

        [SerializeField] private Text descElement;
        public Text DescElement { get { return descElement; } }


        AchievementData Achievement { get; set; }

        float TimePassed { get; set; }

        public void SetAchievement(AchievementData achievement)
        {
            this.Achievement = achievement;

            this.ImageElement.sprite = this.Achievement.Image;
            this.TitleElement.text = this.Achievement.Title;
            this.DescElement.text = this.Achievement.Desc;
        }

        private void Update()
        {
            this.TimePassed += Time.deltaTime;

            float passed = this.TimePassed;

            if (passed <= .5f)
            {
                this.transform.localScale = new Vector3(1, 1 * passed * 2f, 1);
            }

            float remaining = GameSettings.AchievementPanelDuration - this.TimePassed;

            if (remaining <= 0)
            {
                GameObject.Destroy(this.gameObject);
                return;
            }

            if (remaining <= 1)
            {
                this.transform.localScale = new Vector3(1, remaining, 1);
            }
        }
    }
}
