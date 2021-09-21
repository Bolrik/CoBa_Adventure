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
    public class ColorDoor : InteractiveObject, IColorSignalReceiver
    {
        public static ColorDoor Create(ColorCode colorCode, bool isClosed, bool isInvisible = false)
        {
            ColorDoor instance = GameObject.Instantiate(GameAssets.ColorDoor);
            instance.ColorCode = colorCode;
            instance.IsClosed = isClosed; 
            instance.IsInvisible = isInvisible;


            return instance;
        }

        [Header("Info")]
        [SerializeField] private ColorCode colorCode;
        public ColorCode ColorCode { get { return colorCode; } private set { colorCode = value; } }

        [SerializeField] private bool isClosed = false;
        public bool IsClosed { get { return isClosed; } private set { isClosed = value; } }

        [SerializeField] private bool isInvisible;
        public bool IsInvisible { get { return isInvisible; } private set { isInvisible = value; } }

        [Header("Config")]
        [SerializeField] private Collider2D[] colliders;
        public Collider2D[] Colliders { get { return colliders; } private set { colliders = value; } }

        [Header("Visuals")]
        [SerializeField] private SpriteRenderer bodyRenderer;
        public SpriteRenderer BodyRenderer { get { return bodyRenderer; } private set { bodyRenderer = value; } }

        public override bool TriggerReaction => false;

        private void Start()
        {
            ColorSignalManager.Register(this);

            this.SetIsClosed(this.IsClosed);
        }

        public void UpdateSignal(ColorCode colorCode)
        {
            if (colorCode != this.ColorCode)
                return;

            this.SetIsClosed(!this.IsClosed);
        }

        public override void OnTouch(ColorBall colorBall)
        {

        }

        private void Update()
        {
            this.UpdateVisuals();
        }


        private void SetIsClosed(bool value)
        {
            this.IsClosed = value;

            this.UpdateVisuals();

            foreach (var collider in this.Colliders)
            {
                collider.enabled = value;
            }
        }

        private void UpdateVisuals()
        {
            if (this.IsInvisible)
            {
                this.BodyRenderer.sprite = GameAssets.WallSprite;
                this.BodyRenderer.color = Color.white;

                return;
            }

            Color color = this.ColorCode.GetColor();

            int spriteIdx = this.IsClosed ? 0 : 1;
            this.BodyRenderer.sprite = GameAssets.ColorDoorSprites[spriteIdx];

            color.a = this.IsClosed ? 1 : .5f;
            this.BodyRenderer.color = color;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!this.IsInvisible)
                return;

            Debug.Log(collider);

            if (collider.GetComponentInParent<ColorBall>() is ColorBall colorBall)
            {
                this.IsInvisible = false;
                this.UpdateVisuals();
            }
        }
    }
}