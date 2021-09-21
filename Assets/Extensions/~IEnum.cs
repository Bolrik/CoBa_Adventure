using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace System.Collections.Generic
{
    public static partial class Extensions
    {
        public static string Aggregate<T>(this IEnumerable<T> enumerable, string separation, Func<T, string> tToString)
        {
            return enumerable.Aggregate("", (x, y) => x.Length == 0 ? tToString(y) : $"{x}{separation}{tToString(y)}");
        }
    }
}
