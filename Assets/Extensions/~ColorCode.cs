using Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityEngine
{
    public static partial class Extensions
    {
        public static Color GetColor(this ColorCode colorCode)
        {
            switch (ColorCodeManager.ColorMode)
            {
                case ColorMode.RYB:
                    switch (colorCode)
                    {
                        case ColorCode.None:
                            return Color.white;

                        case ColorCode.Red:
                            return Color.red;
                        case ColorCode.Green:
                            return Color.yellow;
                        case ColorCode.Blue:
                            return Color.blue;

                        case ColorCode.Yellow:
                            return ColorCodeManager.Orange;
                        case ColorCode.Cyan:
                            return Color.green;
                        case ColorCode.Magenta:
                            return ColorCodeManager.Purple;

                        case ColorCode.Black:
                            return Color.black;
                        default:
                            throw new NotImplementedException();
                    }
                case ColorMode.RGB:
                    switch (colorCode)
                    {
                        case ColorCode.None:
                            return Color.white;

                        case ColorCode.Red:
                            return Color.red;
                        case ColorCode.Green:
                            return Color.green;
                        case ColorCode.Blue:
                            return Color.blue;

                        case ColorCode.Yellow:
                            return Color.yellow;
                        case ColorCode.Cyan:
                            return Color.cyan;
                        case ColorCode.Magenta:
                            return Color.magenta;

                        case ColorCode.Black:
                            return Color.black;
                        default:
                            throw new NotImplementedException();
                    }
                case ColorMode.CMYK:
                    switch (colorCode)
                    {
                        case ColorCode.None:
                            return Color.white;

                        case ColorCode.Red:
                            return Color.magenta;
                        case ColorCode.Green:
                            return Color.yellow;
                        case ColorCode.Blue:
                            return Color.cyan;

                        case ColorCode.Yellow:
                            return Color.red;
                        case ColorCode.Cyan:
                            return Color.green;
                        case ColorCode.Magenta:
                            return Color.blue;

                        case ColorCode.Black:
                            return Color.black;
                        default:
                            throw new NotImplementedException();
                    }

                default:
                    throw new NotImplementedException();
            }
        }

        public static ColorCode Mix(this ColorCode colorCode, ColorCode other)
        {
            if (colorCode == other ||
                colorCode > ColorCode.Blue)
                return colorCode;

            switch (colorCode)
            {
                case ColorCode.None:
                    return other;
                case ColorCode.Red:
                    switch (other)
                    {
                        case ColorCode.Green:
                            return ColorCode.Yellow;
                        case ColorCode.Blue:
                            return ColorCode.Magenta;
                        default:
                            throw new NotImplementedException();
                    }
                case ColorCode.Green:
                    switch (other)
                    {
                        case ColorCode.Red:
                            return ColorCode.Yellow;
                        case ColorCode.Blue:
                            return ColorCode.Cyan;
                        default:
                            throw new NotImplementedException();
                    }
                case ColorCode.Blue:
                    switch (other)
                    {
                        case ColorCode.Red:
                            return ColorCode.Magenta;
                        case ColorCode.Green:
                            return ColorCode.Cyan;
                        default:
                            throw new NotImplementedException();
                    }
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
