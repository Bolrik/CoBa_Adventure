using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Basics
{
    public class GameSettings : MonoBehaviour
    {
        #region Singleton Pattern		
        static GameSettings Instance { get; set; }

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

        [Header("Grabbable Objects Settings")]
        [SerializeField] private LayerMask grabbableObjectsLayer;
        public static LayerMask GrabbableObjectsLayer { get { return Instance.grabbableObjectsLayer; } }

        [Header("Interaction Settings")]
        [SerializeField] private LayerMask interactiveLayer;
        public static LayerMask InteractiveLayer { get { return Instance.interactiveLayer; } }

        [Header("Slow Motion Settings")]
        [SerializeField] private LayerMask slowMotionLayer;
        public static LayerMask SlowMotionLayer { get { return Instance.slowMotionLayer; } }

        [SerializeField] private float slowMotionRange = 2;
        public static float SlowMotionRange { get { return Instance.slowMotionRange; } }

        [SerializeField] private float slowMotionPeak = .5f;
        public static float SlowMotionPeak { get { return Instance.slowMotionPeak; } }

        [Header("Game Speed Settings")]
        [SerializeField] private float gameSpeedMin = .25f;
        public static float GameSpeedMin { get { return Instance.gameSpeedMin; } }

        [Header("Achievements")]
        [SerializeField] private float achievementPanelDuration = 6f;
        public static float AchievementPanelDuration { get { return Instance.achievementPanelDuration; } }

    }
}