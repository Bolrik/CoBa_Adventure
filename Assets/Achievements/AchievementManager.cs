using Level;
using PlayerInteraction;
using SlingMechanic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Templates;
using UnityEngine;

namespace Achievements
{
    public class AchievementManager : MonoBehaviour
    {
        #region Singleton Pattern		
        public static AchievementManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                if (Instance == this)
                    return;

                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion


        [SerializeField] private GameObject achievementPanelContainer;
        public GameObject AchievementPanelContainer { get { return achievementPanelContainer; } }



        Dictionary<AchievementType, bool> Completed { get; set; } = new Dictionary<AchievementType, bool>();

        public void Set(AchievementType achievementType)
        {
            if (this.Completed.ContainsKey(achievementType) && this.Completed[achievementType])
                return;

            var achievement = AchievementAssets.Achievements.FirstOrDefault(x => x.Type == achievementType);

            if (achievement == null)
                return;

            this.Completed[achievementType] = true;
            this.ShowAchievement(achievement);
        }


        private void ShowAchievement(AchievementData achievement)
        {
            if (achievement == null)
                return;

            Debug.Log($"Achievement done: {achievement.Title}");
            this.OpenAchievementPanel(achievement);
        }

        private void OpenAchievementPanel(AchievementData achievement)
        {
            AchievementPanel panel = GameObject.Instantiate(AchievementAssets.AchievementPanel);
            panel.transform.SetParent(this.AchievementPanelContainer.transform, false);
            panel.SetAchievement(achievement);
        }

        private void Update()
        {
            AchievementChecker.Instance.Check();

            if (Input.GetKeyDown(KeyCode.A))
                this.DebugReset();
        }

        private void DebugReset()
        {
            for (int i = 0; i < this.Completed.Count; i++)
            {
                var achievement = this.Completed.ElementAt(i);
                this.Completed[achievement.Key] = false;
            }
        }

        public Dictionary<AchievementType, bool> GetAchievements()
        {
            return this.Completed;
        }
    }

    public class AchievementChecker
    {
        #region Singleton Pattern
        public static AchievementChecker Instance { get; private set; }
        static AchievementChecker()
        {
            new AchievementChecker();
        }

        private AchievementChecker()
        {
            Instance = this;

            this.RegisterEvents();
        }

        void RegisterEvents()
        {
            // Level Manager
            LevelManager.Instance.PreLoadLevel += this.LevelManager_OnPreLoadLevel;
            LevelManager.Instance.LevelDone += this.LevelManager_OnLevelDone;
            LevelManager.Instance.RestartLevel += this.LevelManager_OnRestartLevel;

            // Grab Manager
            GrabManager.Instance.GrabStart += this.GrabManager_OnGrabStart;
            GrabManager.Instance.GrabReleased += this.GrabManager_OnGrabReleased;


#warning #5 ToDo: Unregister events when all are done...
        }
        #endregion

        /// <see cref="AchievementType.HoleInOne"/>
        int LevelSlingCount { get; set; }

        /// <see cref="AchievementType.FiveTimesAlready"/>
        int RestartCounter { get; set; }


        public void Check()
        {
            
        }

        private void LevelManager_OnPreLoadLevel()
        {
            /// <see cref="AchievementType.HoleInOne"/>
            this.LevelSlingCount = 0;
        }

        private void LevelManager_OnLevelDone(LevelDoneEventArgs eventArgs)
        {
            /// <see cref="AchievementType.HoleInOne"/>
            if (this.LevelSlingCount <= 1)
                this.Set(AchievementType.HoleInOne);

            /// <see cref="AchievementType.DeadManWalking"/>
            if (!eventArgs.FinishedBy.ColorBallInfo.IsAlive)
                this.Set(AchievementType.DeadManWalking);

            this.RestartCounter = 0;
        }

        private void LevelManager_OnRestartLevel()
        {
            this.RestartCounter++;

            if (this.RestartCounter >= 5)
                this.Set(AchievementType.FiveTimesAlready);
        }

        private void GrabManager_OnGrabStart(IGrabObject grabObject)
        {
            /// <see cref="AchievementType.GrabACoBa"/>
            if (grabObject is ColorBall)
                this.Set(AchievementType.GrabACoBa);
        }

        private void GrabManager_OnGrabReleased(IGrabObject grabObject)
        {
            /// <see cref="AchievementType.HoleInOne"/>
            this.LevelSlingCount++;
        }



        private void Set(AchievementType achievementType) => AchievementManager.Instance.Set(achievementType);
    }

    public enum AchievementType
    {
        // Basics
        GrabACoBa,
        HoleInOne,
        DeadManWalking,
        FiveTimesAlready,
        TheSecret,

        // Blocks / Tiles
        HitTheButton,
        AllTheColors
    }
}