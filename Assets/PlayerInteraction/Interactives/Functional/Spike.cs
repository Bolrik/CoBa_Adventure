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
    public class Spike : InteractiveObject, IColorSignalReceiver
    {
        public static Spike Create(ColorCode colorCode, bool isActive)
        {
            Spike instance = GameObject.Instantiate(GameAssets.Spike);
            instance.ColorCode = colorCode;
            instance.IsActive = isActive;

            return instance;
        }

        [SerializeField] private SpriteRenderer spriteRenderer;
        public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }

        [SerializeField] private SpriteRenderer colorCodeRenderer;
        public SpriteRenderer ColorCodeRenderer { get { return colorCodeRenderer; } }


        public bool IsActive { get; private set; }

        public ColorCode ColorCode { get; private set; }


        private void Start()
        {
            ColorSignalManager.Register(this);

            this.SetIsActive(this.IsActive);
        }

        private void Update()
        {
            this.UpdateVisuals();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!this.IsActive)
                return;

            if (collision.GetComponentInParent<ColorBall>() is ColorBall colorBall)
            {
                colorBall.Die();
            }
        }

        private void SetIsActive(bool isActive)
        {
            this.IsActive = isActive;
        }

        public void UpdateSignal(ColorCode colorCode)
        {
            if (colorCode != this.ColorCode)
                return;

            this.SetIsActive(!this.IsActive);
        }

        private void UpdateVisuals()
        {
            Color color = this.ColorCode.GetColor();
            this.ColorCodeRenderer.color = color;

            int spriteIdx = this.IsActive ? 0 : 1;
            this.SpriteRenderer.sprite = GameAssets.SpikeSprites[spriteIdx];

            color = Color.white;
            color.a = this.IsActive ? 1 : .5f;
            this.SpriteRenderer.color = color;
        }
    }
}