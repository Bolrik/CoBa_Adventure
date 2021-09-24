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
        public static int PingPong(this int input, int min, int max)
        {
            int range = max - min;
            return min + Math.Abs(((input + range) % (range * 2)) - range);
        }
    }
}
