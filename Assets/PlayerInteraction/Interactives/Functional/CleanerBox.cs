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
    public class CleanerBox : InteractiveObject
    {
        public static CleanerBox Create(ColorCode colorCode)
        {
            CleanerBox instance = GameObject.Instantiate(GameAssets.CleanerBox);
            instance.ColorCode = colorCode;

            return instance;
        }

        [Header("Visuals")]
        [SerializeField] private SpriteRenderer bodyRenderer;
        public SpriteRenderer BodyRenderer { get { return bodyRenderer; } private set { bodyRenderer = value; } }


        public ColorCode ColorCode { get; private set; }

        protected override void OnTouch(ColorBall colorBall)
        {
            ColorCode otherCode = colorBall.ColorCode;
            ColorCode thisCode = this.ColorCode;

            // We have none - take theires
            if (thisCode == ColorCode.None)
            {
                this.ColorCode = otherCode;
                colorBall.SetColorCode(ColorCode.None);
            }
            else if (otherCode == ColorCode.None)
            {
                this.ColorCode = ColorCode.None;
                colorBall.SetColorCode(thisCode);
            }
            // Both in Color Stage 1 (RGB), Mix and remove from Ball
            else if (thisCode <= ColorCode.Blue && otherCode <= ColorCode.Blue)
            {
                if (thisCode == otherCode) return;

                this.ColorCode = thisCode.Mix(otherCode);
                colorBall.SetColorCode(ColorCode.None);
            }
            // This Color Stage 2 (YMC) - Do Nothing
            else if (thisCode >= ColorCode.Yellow)
            {
                return;
            }
            // Other Color Stage 2, Ours will be <= Stage 1 - Take Theires, Clear Ball
            else if (otherCode >= ColorCode.Yellow)
            {
                this.ColorCode = otherCode;

                colorBall.SetColorCode(ColorCode.None);
            }



//            // This is without Color - Get from Ball
//            if (this.ColorCode == ColorCode.None)
//            {
//                this.ColorCode = colorBall.ColorCode;

//                colorBall.SetColorCode(ColorCode.None);
//            }
//            // 
//            else if (this.ColorCode <= ColorCode.Blue)
//            {
//                this.ColorCode = this.ColorCode.Mix(colorBall.ColorCode);

//                colorBall.SetColorCode(ColorCode.None);
//            }
//// #error REWORKKKK

//            // CoBa has no Color - Give it mine
//            if (colorBall.ColorCode == ColorCode.None)
//            {
//                colorBall.SetColorCode(this.ColorCode);
//                this.ColorCode = ColorCode.None;
//            }
//            // Same Color - Do nothing
//            else if (this.ColorCode == colorBall.ColorCode)
//            {
//#warning Rework!!!
//                return;
//            }
//            // CoBa has Color - Remove theires and mix into mine!
//            else
//            {
//                if (this.ColorCode != ColorCode.Black)
//                    this.ColorCode = this.ColorCode.Mix(colorBall.ColorCode);

//                colorBall.SetColorCode(ColorCode.None);
//            }
        }


        private void Update()
        {
            this.BodyRenderer.color = this.ColorCode.GetColor();
        }
    }
}