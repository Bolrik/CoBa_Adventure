using Achievements;
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
    public class ColorBox : InteractiveObject
    {
        public static ColorBox Create(ColorCode colorCode)
        {
            ColorBox instance = GameObject.Instantiate(GameAssets.ColorBox);
            instance.ColorCode = colorCode;

            return instance;
        }

        [Header("Info")]
        [SerializeField] private ColorCode colorCode;
        public ColorCode ColorCode { get { return colorCode; } private set { colorCode = value; } }

        [Header("Visuals")]
        [SerializeField] private SpriteRenderer bodyRenderer;
        public SpriteRenderer BodyRenderer { get { return bodyRenderer; } private set { bodyRenderer = value; } }


        protected override void OnTouch(ColorBall colorBall)
        {
            if (colorBall != null)
            {
                AchievementManager.Instance.Set(AchievementType.AllTheColors);
            }

            colorBall.AddColorCode(this.ColorCode);
        }

        private void Update()
        {
            this.BodyRenderer.color = this.ColorCode.GetColor();
        }
    }
}