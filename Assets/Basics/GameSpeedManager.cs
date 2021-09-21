using PlayerInteraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Basics
{
    public class GameSpeedManager
    {
        #region Singleton Pattern
        public static GameSpeedManager Instance { get; private set; }
        static GameSpeedManager()
        {
            new GameSpeedManager();
        }

        private GameSpeedManager()
        {
            Instance = this;
        }
        #endregion

        public float GameSpeed { get; private set; } = 1;

        public void SetGameSpeed(float percent)
        {
            const float defaultFixedScale = 1f / 50f;

            float finalValue = GameSettings.GameSpeedMin + ((1 - GameSettings.GameSpeedMin) * Mathf.Clamp01(percent));

            Time.timeScale = finalValue;
            Time.fixedDeltaTime = Time.timeScale * defaultFixedScale;

            // this.GameSpeed = finalValue;
        }

        public float UpdateGameSpeed()
        {
            float gameSpeed = 1;

            foreach (var colorBall in ColorBallManager.Instance.GetActiveColorBalls())
            {
                float desiredGameSpeed = colorBall.GetDesiredGameSpeed();

                if (desiredGameSpeed < gameSpeed)
                    gameSpeed = desiredGameSpeed;
            }

            this.SetGameSpeed(gameSpeed);

            return gameSpeed;
        }
    }
}