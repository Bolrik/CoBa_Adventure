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
    public class Sensor : InteractiveObject
    {
        public static Sensor Create(int scaleX, int scaleY)
        {
            Sensor instance = GameObject.Instantiate(GameAssets.Sensor);
            instance.SpriteRenderer.size = new Vector2(scaleX, scaleY);
            instance.TriggerBox.size = new Vector2(scaleX, scaleY);

            return instance;
        }

        [SerializeField] private SpriteRenderer spriteRenderer;
        SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }

        [SerializeField] private BoxCollider2D triggerBox;
        public BoxCollider2D TriggerBox { get { return triggerBox; } }

        public override bool TriggerReaction => false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponentInParent<ColorBall>() is ColorBall colorBall)
            {
                ColorSignalManager.SendSignal(colorBall.ColorCode);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponentInParent<ColorBall>() is ColorBall colorBall)
            {
                ColorSignalManager.SendSignal(colorBall.ColorCode);
            }
        }
    }
}