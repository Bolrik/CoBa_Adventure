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
    public class DetectorDoor : InteractiveObject, IGrabSignalReceiver, IColorSignalReceiver
    {
        public static DetectorDoor Create(ColorCode colorCode, int timer, bool isClosed)
        {
            DetectorDoor instance = GameObject.Instantiate(GameAssets.DetectorDoor);
            instance.ColorCode = colorCode;
            instance.TimerMax = instance.Timer = timer;
            instance.DefaultState = instance.IsClosed = isClosed;

            return instance;
        }

        [Header("Config")]
        [SerializeField] private Collider2D defaultCollider;
        public Collider2D DefaultCollider { get { return defaultCollider; } private set { defaultCollider = value; } }

        [Header("Visuals")]
        [SerializeField] private SpriteRenderer bodyRenderer;
        public SpriteRenderer BodyRenderer { get { return bodyRenderer; } private set { bodyRenderer = value; } }

        int TimerMax { get; set; }
        int Timer { get; set; }
        bool IsTriggered { get; set; }
        bool DefaultState { get; set; }

        public bool IsClosed { get; private set; }

        public ColorCode ColorCode { get; set; }

        private void Start()
        {
            GrabManager.Instance.Register(this);
            ColorSignalManager.Register(this);

            this.ResetDoor();
        }

        public void UpdateSignal(IGrabObject grabObject)
        {
            if (this.Count())
                this.SetIsClosed(!this.IsClosed);
        }

        public void UpdateSignal(ColorCode colorCode)
        {
            if (colorCode == this.ColorCode)
            {
                this.ResetDoor();
            }
        }

        private void ResetDoor()
        {
            this.Timer = this.TimerMax;
            this.IsTriggered = false;
            this.SetIsClosed(this.DefaultState);
        }

        private bool Count()
        {
            if (this.IsTriggered)
                return false;

            this.Timer--;

            bool result = this.Timer == 0;

            this.IsTriggered = result;

            return result;
        }

        private void SetIsClosed(bool value)
        {
            this.IsClosed = value;

            this.UpdateVisuals();

            this.DefaultCollider.enabled = value;
        }

        private void Update()
        {
            this.UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            int spriteIdx = 0;

            if (this.IsTriggered || System.DateTime.Now.Second % 2 < 1)
            {
                spriteIdx = this.IsClosed ? 0 : 1;
                this.BodyRenderer.sprite = GameAssets.DetectorDoorSprites[spriteIdx];
            }
            else
            {
                spriteIdx = this.Timer;

                if (spriteIdx > 9)
                {
                    spriteIdx = 9;

                    int ms = System.DateTime.Now.Millisecond;
                    if (ms >= 700)
                        spriteIdx = 0;
                }

                this.BodyRenderer.sprite = GameAssets.NumberSprites[spriteIdx];
            }

            Color color = this.ColorCode.GetColor();
            color.a = this.IsClosed ? 1 : .5f;
            this.BodyRenderer.color = color;
        }
    }
}