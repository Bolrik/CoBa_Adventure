using Basics;
using Level;
using PlayerInteraction.Interactives;
using SlingMechanic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerInteraction
{
    public class ColorBallManager
    {
        #region Singleton Pattern
        public static ColorBallManager Instance { get; private set; }
        static ColorBallManager()
        {
            new ColorBallManager();
        }

        private ColorBallManager()
        {
            Instance = this;
            LevelManager.Instance.PreLoadLevel += this.ReInitialize;
        }
        #endregion

        List<ColorBall> ActiveColorBalls { get; set; } = new List<ColorBall>();

        public void Register(ColorBall colorBall)
        {
            this.ActiveColorBalls.Add(colorBall);
        }

        public void Unregister(ColorBall colorBall)
        {
            this.ActiveColorBalls.Remove(colorBall);
        }

        public IEnumerable<ColorBall> GetActiveColorBalls()
        {
            foreach (var colorBall in this.ActiveColorBalls)
            {
                yield return colorBall;
            }
        }


        void ReInitialize()
        {
            this.ActiveColorBalls.Clear();
        }
    }
}