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
    public class Wall : InteractiveObject
    {
        public static Wall Create()
        {
            Wall instance = GameObject.Instantiate(GameAssets.Wall);

            return instance;
        }
    }
}