using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Basics
{
    public class ColorCodeManager
    {
        #region Singleton Pattern
        public static ColorCodeManager Instance { get; private set; }
        static ColorCodeManager()
        {
            new ColorCodeManager();
        }

        private ColorCodeManager()
        {
            Instance = this;
        }
        #endregion


        [SerializeField] private ColorMode colorMode = ColorMode.RYB;
        public static ColorMode ColorMode { get { return Instance.colorMode; } private set { Instance.colorMode = value; } }

        public void SetColorMode(ColorMode colorMode) => ColorCodeManager.ColorMode = colorMode;

        public static Color Orange { get; } = new Color(1, .65f, 0);
        public static Color Purple { get; } = new Color(.5f, 0, .5f);
    }

    public enum ColorMode
    {
        RYB,
        RGB,
        CMYK
    }

}