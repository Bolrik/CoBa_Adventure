using Basics;
using SlingMechanic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerInteraction.Interactives
{
    public class HealBox : InteractiveObject
    {
        public static HealBox Create()
        {
            HealBox instance = GameObject.Instantiate(GameAssets.HealBox);

            return instance;
        }

        protected override void OnTouch(ColorBall colorBall)
        {
            if (colorBall.ColorBallInfo.IsAlive)
                return;

            colorBall.Revive();
        }
    }
}