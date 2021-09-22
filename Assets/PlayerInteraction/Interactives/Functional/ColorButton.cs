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
    public class ColorButton : InteractiveObject
    {
        public static ColorButton Create(ColorCode colorCode)
        {
            ColorButton instance = GameObject.Instantiate(GameAssets.ColorDetector);
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
                AchievementManager.Instance.Set(AchievementType.HitTheButton);
            }

            if (this.ColorCode == ColorCode.None)
            {
                this.ColorCode = colorBall.ColorCode;
                colorBall.SetColorCode(ColorCode.None);
                ColorSignalManager.SendSignal(this.ColorCode);
            }
            else if (colorBall.ColorCode == this.ColorCode)
            {
                ColorSignalManager.SendSignal(this.ColorCode);
            }
        }

        private void Update()
        {
            this.BodyRenderer.color = this.ColorCode.GetColor();
        }
    }
}