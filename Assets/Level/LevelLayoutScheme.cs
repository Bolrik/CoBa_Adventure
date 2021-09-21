using System;
using UnityEngine;

namespace Level
{
    public class LevelLayoutScheme
    {
        public int Width { get => this.Objects.GetLength(0); }
        public int Height { get => this.Objects.GetLength(1); }

        LevelLayoutSchemeData[,] Objects { get; set; }

        public LevelLayoutScheme(int scaleX, int scaleY)
        {
            this.Objects = new LevelLayoutSchemeData[scaleX, scaleY];
        }


        public void Add(Func<MonoBehaviour> create, int sX, int sY, int eX, int eY)
        {
            int maxX = Mathf.Max(sX, eX);
            int minX = Mathf.Min(sX, eX);

            int maxY = Mathf.Max(sY, eY);
            int minY = Mathf.Min(sY, eY);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    this.Add(new LevelLayoutSchemeData(create), x, y);
                }
            }
        }

        public void Add(Func<MonoBehaviour> create, int x, int y)
        {
            this.Add(new LevelLayoutSchemeData(create), x, y);
        }

        public void Add(Func<MonoBehaviour> create, float x, float y)
        {
            int iX = (int)x, iY = (int)y;
            float offsetX = x % 1f;
            float offsetY = y % 1f;

            this.Add(new LevelLayoutSchemeData(create, offsetX, offsetY), iX, iY);
        }

        private void Add(LevelLayoutSchemeData levelLayoutSchemeData, int x, int y)
        {
            this.Objects[x, y] = levelLayoutSchemeData;
        }

        public LevelLayoutSchemeData Get(int x, int y)
        {
            return this.Objects[x, y];
        }
    }
}
