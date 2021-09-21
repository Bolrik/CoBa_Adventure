using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Templates;
using UnityEngine;

namespace Achievements
{
    [System.Serializable]
    public class AchievementAssets : MonoBehaviour
    {
        #region Singleton Pattern		
        static AchievementAssets Instance { get; set; }

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

        [SerializeField] private AchievementData[] achievements;
        public static AchievementData[] Achievements { get { return Instance.achievements; } }

        [SerializeField] private AchievementPanel achievementPanel;
        public static AchievementPanel AchievementPanel { get { return Instance.achievementPanel; } }
    }
}