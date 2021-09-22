using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_017 : Level
    {
        #region Singleton Pattern
        public static Level_017 Instance { get; private set; }
        static Level_017()
        {
            new Level_017();
        }

        private Level_017()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 9;
        public override int ScaleY => 9;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 3, 0, 3, 2);
            scheme.Add(() => Wall.Create(), 3, 6, 3, 8);
            scheme.Add(() => Wall.Create(), 4, 6, 8, 6);
            scheme.Add(() => Wall.Create(), 7, 2, 8, 2);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 8, 0);
            scheme.Add(() => GoalFlag.Create(1, 0), 6, 8); // Enter World 2

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.None), 1, 2);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Green), 1, 8);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.Green), 1, 0);
            scheme.Add(() => ColorButton.Create(ColorCode.None), 8, 4);

            // Cleaner

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, true), 4, 2, 6, 2);

            // Hidden Doors
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, false, true), 8, 6, 8, 6);

            // DDoor
            scheme.Add(() => DetectorDoor.Create(ColorCode.None, 2, false), 0, 6, 2, 6);
            scheme.Add(() => DetectorDoor.Create(ColorCode.None, 4, false), 3, 3, 3, 5);

            // Death
            scheme.Add(() => DeathBox.Create(), 4, 8);
            scheme.Add(() => DeathBox.Create(), 5, 7);
            scheme.Add(() => DeathBox.Create(), 7, 7);
        }
    }
}
