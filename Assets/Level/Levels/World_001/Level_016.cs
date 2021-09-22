using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_016 : Level
    {
        #region Singleton Pattern
        public static Level_016 Instance { get; private set; }
        static Level_016()
        {
            new Level_016();
        }

        private Level_016()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 13;
        public override int ScaleY => 11;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 3, 3, 8, 3);
            scheme.Add(() => Wall.Create(), 3, 7, 5, 7);
            scheme.Add(() => Wall.Create(), 7, 7, 8, 7);
            scheme.Add(() => Wall.Create(), 9, 4, 9, 6);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 7, 5);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.None), 1, 5);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Blue), 6, 7);
            scheme.Add(() => ColorBox.Create(ColorCode.Green), 12, 10);
            scheme.Add(() => ColorBox.Create(ColorCode.Red), 0, 0);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.Blue), 0, 10);
            scheme.Add(() => ColorButton.Create(ColorCode.Green), 9, 7);
            scheme.Add(() => ColorButton.Create(ColorCode.Red), 9, 3);

            // Cleaner
            scheme.Add(() => CleanerBox.Create(ColorCode.None), 12, 5);

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Blue, true), 5, 4, 5, 6);
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, true), 4, 4, 4, 6);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, true), 3, 4, 3, 6);

            // DDoor
            scheme.Add(() => DetectorDoor.Create(ColorCode.None, 11, false), 3, 0, 3, 2);
            scheme.Add(() => DetectorDoor.Create(ColorCode.None, 11, false), 3, 8, 3, 10);
        }
    }
}
