using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_012 : Level
    {
        #region Singleton Pattern
        public static Level_012 Instance { get; private set; }
        static Level_012()
        {
            new Level_012();
        }

        private Level_012()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 13;
        public override int ScaleY => 8;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 0, 7, 5, 7);
            scheme.Add(() => Wall.Create(), 3, 3, 7, 3);
            scheme.Add(() => Wall.Create(), 7, 7, 11, 7);
            scheme.Add(() => Wall.Create(), 9, 0, 9, 3);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 11, 1);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.Green), 1, 3);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Red), 0, 6);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.None), 12, 7);
            scheme.Add(() => ColorButton.Create(ColorCode.None), 6, 7);

            // Cleaner
            scheme.Add(() => CleanerBox.Create(ColorCode.None), 8, 3);

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, true), 10, 3, 12, 3);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, false), 3, 4, 3, 6);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, true), 7, 4, 7, 6);
        }
    }
}
