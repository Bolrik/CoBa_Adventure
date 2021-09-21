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
    public class DetectorDoor : InteractiveObject, IGrabSignalReceiver
    {
        public static DetectorDoor Create(int detectionCount, bool isClosed)
        {
            DetectorDoor instance = GameObject.Instantiate(GameAssets.DetectorDoor);
            instance.DetectionCountMax = instance.DetectionCount = detectionCount;
            instance.IsClosed = isClosed;

            return instance;
        }

        [Header("Info")]
        [SerializeField] private bool isClosed = false;
        public bool IsClosed { get { return isClosed; } private set { isClosed = value; } }

        [SerializeField] private int detectionCount;
        public int DetectionCount { get { return detectionCount; } private set { detectionCount = value; } }

        [SerializeField] private int detectionCountMax = 3;
        public int DetectionCountMax { get { return detectionCountMax; } private set { detectionCountMax = value; } }

        [Header("Config")]
        [SerializeField] private Collider2D defaultCollider;
        public Collider2D DefaultCollider { get { return defaultCollider; } private set { defaultCollider = value; } }

        [Header("Visuals")]
        [SerializeField] private SpriteRenderer bodyRenderer;
        public SpriteRenderer BodyRenderer { get { return bodyRenderer; } private set { bodyRenderer = value; } }


        bool IsTriggered { get; set; }


        private void Start()
        {
            GrabManager.Instance.Register(this);

            this.DetectionCount = this.DetectionCountMax;
            this.SetIsClosed(this.IsClosed);
        }

        public void UpdateSignal(IGrabObject grabObject)
        {
            if (this.Count())
                this.SetIsClosed(!this.IsClosed);
        }

        private bool Count()
        {
            if (this.IsTriggered)
                return false;

            this.DetectionCount--;

            bool result = this.DetectionCount == 0;

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
                spriteIdx = this.DetectionCount;

                if (spriteIdx > 9)
                {
                    spriteIdx = 9;

                    int ms = System.DateTime.Now.Millisecond;
                    if (ms >= 700)
                        spriteIdx = 0;
                }

                this.BodyRenderer.sprite = GameAssets.NumberSprites[spriteIdx];
            }

            Color color = Color.white; ;
            color.a = this.IsClosed ? 1 : .5f;
            this.BodyRenderer.color = color;
        }
    }
}