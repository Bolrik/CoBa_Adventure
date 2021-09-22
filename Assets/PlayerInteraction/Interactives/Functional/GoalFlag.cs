using Basics;
using Level;
using SlingMechanic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerInteraction.Interactives
{
    public class GoalFlag : InteractiveObject
    {
        public static GoalFlag Create(int world = -1, int level = -1)
        {
            GoalFlag instance = GameObject.Instantiate(GameAssets.GoalFlag);
            instance.WorldIndex = world;
            instance.LevelIndex = level;

            return instance;
        }

        public int WorldIndex { get; private set; }
        public int LevelIndex { get; private set; }

        protected override void OnTouch(ColorBall colorBall)
        {
            LevelManager.Instance.MarkAsDone(this, colorBall);
        }
    }
}