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
    public class Ground : InteractiveObject
    {
        public static Ground Create(int scaleX, int scaleY)
        {
            Ground instance = GameObject.Instantiate(GameAssets.Ground);
            instance.SpriteRenderer.size = new Vector2(scaleX, scaleY);

            return instance;
        }

        [SerializeField] private SpriteRenderer spriteRenderer;
        SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }



        public override void OnTouch(ColorBall colorBall)
        {

        }
    }
}