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
    public class DeathBox : InteractiveObject
    {
        public static DeathBox Create()
        {
            DeathBox instance = GameObject.Instantiate(GameAssets.DeathBox);

            return instance;
        }

        public override void OnTouch(ColorBall colorBall)
        {
            colorBall.Die();
        }
    }
}