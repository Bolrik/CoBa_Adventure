using Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace System
{
    public static partial class Extensions
    {
        public static float PingPong(this float input, float min, float max)
        {
            float range = max - min;
            return min + Math.Abs(((input + range) % (range * 2)) - range);
        }
    }
}
