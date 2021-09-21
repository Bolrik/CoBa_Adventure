using PlayerInteraction.Interactives;
using UnityEngine;

namespace Level
{
    public class LevelLayout
    {
        GameObject Root { get; set; }
        GameObject WallRoot { get; set; }

        MonoBehaviour[,] Objects { get; set; }

        LevelLayoutScheme Scheme { get; set; }

        public LevelLayout(LevelLayoutScheme levelLayoutScheme)
        {
            this.Root = new GameObject("Level Layout");
            this.WallRoot = new GameObject("Walls");
            this.WallRoot.transform.SetParent(this.Root.transform);

            this.Scheme = levelLayoutScheme;

            this.Objects = new MonoBehaviour[this.Scheme.Width + 2, this.Scheme.Height + 2];

            this.Build();
        }

        public void Destroy()
        {
            GameObject.Destroy(this.Root);
        }


        private void Build()
        {
            this.AddGround();
            this.AddEnclosing();

            for (int x = 0; x < this.Scheme.Width; x++)
            {
                for (int y = 0; y < this.Scheme.Height; y++)
                {
                    int fX = 1 + x;
                    int fY = 1 + y;

                    var schemeData = this.Scheme.Get(x, y);

                    if (schemeData == null)
                        continue;

                    this.Add(schemeData.Factory(), fX + schemeData.OffsetX, fY + schemeData.OffsetY);
                }
            }

            this.Root.transform.position = new Vector3(-1, -1);
        }


        private void AddGround()
        {
            Ground ground = Ground.Create(this.Scheme.Width, this.Scheme.Height);
            this.Add(ground, this.Scheme.Width / 2f + .5f, this.Scheme.Height / 2f + .5f);
        }

        private void AddEnclosing()
        {
            for (int y = 0; y <= this.Scheme.Height + 1; y++)
            {
                this.AddWall(0, y);
                this.AddWall(this.Scheme.Width + 1, y);
            }

            // x = 1 : Exclude already placed Corners!
            for (int x = 1; x < this.Scheme.Width + 1; x++)
            {
                this.AddWall(x, 0);
                this.AddWall(x, this.Scheme.Height + 1);
            }
        }

        private void AddWall(int x, int y)
        {
            Wall wall = Wall.Create();
            wall.transform.SetParent(this.WallRoot.transform);
            wall.transform.position = new Vector3(x, y);
        }

        private void Add(MonoBehaviour obj, float x, float y)
        {
            obj.transform.SetParent(this.Root.transform);
            obj.transform.position = new Vector3(x, y);
        }

    }
}
