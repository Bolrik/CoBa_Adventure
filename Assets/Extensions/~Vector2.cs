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
        public static Vector2 Offset(this Vector2 origin, Vector3 change)
        {
            return new Vector2(origin.x + change.x, origin.y + change.y);
        }

        public static Vector3 ToVector3(this Vector2 vector)
        {
            return new Vector2(vector.x, vector.y);
        }
    }
}
