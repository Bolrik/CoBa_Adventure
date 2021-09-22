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
    public class AchievementOverlayPage : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private TitleCanvas titleCanvas;
        public TitleCanvas TitleCanvas { get { return titleCanvas; } }

        [SerializeField] private GameObject achievementOverlayPanelHost;
        public GameObject AchievementOverlayPanelHost { get { return achievementOverlayPanelHost; } }

        [SerializeField] private GameObject achievementOverlayPanel;
        public GameObject AchievementOverlayPanel { get { return achievementOverlayPanel; } }


        [Header("Prefab")]
        [SerializeField] private AchievementOverlayPanel achievementOverlayPanelPrefab;
        public AchievementOverlayPanel AchievementOverlayPanelPrefab { get { return achievementOverlayPanelPrefab; } }


        float AnimationTime { get; set; }
        bool RunAnimation { get; set; }

        bool IsOpen { get; set; }

        private void Start()
        {
            var completedAchievements = AchievementManager.Instance.GetAchievements();

            foreach (var achievementData in AchievementAssets.Achievements)
            {
                AchievementOverlayPanel achievementOverlayPanel = GameObject.Instantiate(this.AchievementOverlayPanelPrefab);
                achievementOverlayPanel.transform.SetParent(this.AchievementOverlayPanelHost.transform, false);
                achievementOverlayPanel.SetAchievement(achievementData, completedAchievements.ContainsKey(achievementData.Type) && completedAchievements[achievementData.Type]);
            }

            //foreach (var achievement in achievements)
            //{
            //    var achievementData = AchievementAssets.Achievements.FirstOrDefault(x => x.Type == achievement.Key);

            //    AchievementOverlayPanel achievementOverlayPanel = GameObject.Instantiate(this.AchievementOverlayPanelPrefab);
            //    achievementOverlayPanel.transform.SetParent(this.AchievementOverlayPanelHost.transform, false);
            //    achievementOverlayPanel.SetAchievement(achievementData, achievement.Value);
            //}
        }

        public void Show(bool show)
        {
            this.AchievementOverlayPanel.SetActive(show);

            if (this.IsOpen == show)
                return;

            Debug.Log("Animate");

            this.IsOpen = show;

            this.AnimationTime = 0;
            this.RunAnimation = true;

            //if (this.IsOpen)
            //    this.Open();
            //else
            //    this.Close();
        }

        public void Open()
        {
            this.AnimationTime = 0;
            this.RunAnimation = true;
        }

        public void Close()
        {
            this.TitleCanvas.ShowAchievementOverlay(false);
        }

        private void Update()
        {
            if (!this.RunAnimation)
                return;

            this.AnimationTime += Time.deltaTime * 4f;

            float time = this.AnimationTime;
            Debug.Log($"Animating... {this.AnimationTime}");
            if (this.IsOpen)
            {
                this.AchievementOverlayPanel.transform.localScale = new Vector3(1, time, 1);
            }
            else
            {
                this.AchievementOverlayPanel.transform.localScale = new Vector3(1, 1 - time, 1);
            }

            if (time >= 1)
            {
                this.RunAnimation = false;
                return;
            }
        }
    }
}
