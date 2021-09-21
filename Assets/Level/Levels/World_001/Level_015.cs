using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_015 : Level
    {
        #region Singleton Pattern
        public static Level_015 Instance { get; private set; }
        static Level_015()
        {
            new Level_015();
        }

        private Level_015()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 7;
        public override int ScaleY => 10;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 3, 3, 3, 6);
            scheme.Add(() => Wall.Create(), 4, 6, 6, 6);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 5, 4);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.None), 1, 1);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Red), 6, 9);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.Red), 0, 9);

            // Cleaner

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, true), 3, 0, 3, 2);

            // DDoor
            scheme.Add(() => DetectorDoor.Create(5, false), 0, 3, 2, 3);

        }
    }
}
