using Basics;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public abstract class World
    {
        protected List<Level> Levels { get; set; } = new List<Level>();

        public int Count { get => this.Levels.Count; }


        public World()
        {
            this.Initialize();
        }

        public abstract void Initialize();

        public Level Get(int index)
        {
            if (index < 0 || this.Levels.Count <= index)
                return null;

            return this.Levels[index];
        }
    }
}
