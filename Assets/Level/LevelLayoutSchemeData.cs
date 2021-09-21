using System;
using UnityEngine;

namespace Level
{
    public class LevelLayoutSchemeData
    {
        public Func<MonoBehaviour> Factory { get; private set; }
        public float OffsetX { get; private set; }
        public float OffsetY { get; private set; }

        public LevelLayoutSchemeData(Func<MonoBehaviour> factory, float offsetX, float offsetY)
        {
            this.Factory = factory;

            this.OffsetX = offsetX;
            this.OffsetY = offsetY;
        }

        public LevelLayoutSchemeData(Func<MonoBehaviour> factory)
        {
            this.Factory = factory;
        }
    }
}
