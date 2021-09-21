using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_022 : Level
    {
        #region Singleton Pattern
        public static Level_022 Instance { get; private set; }
        static Level_022()
        {
            new Level_022();
        }

        private Level_022()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 10;
        public override int ScaleY => 11;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 3, 3, 4, 3);
            scheme.Add(() => Wall.Create(), 3, 7, 6, 7);
            scheme.Add(() => Wall.Create(), 6, 1, 6, 5);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 8, 1);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.None), 4, 1);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Blue), 6, 6);
            scheme.Add(() => ColorBox.Create(ColorCode.Green), 6, 0);
            scheme.Add(() => ColorBox.Create(ColorCode.Red), 5, 3);

            // Button

            // Cleaner
            scheme.Add(() => CleanerBox.Create(ColorCode.None), 0, 0);
            scheme.Add(() => CleanerBox.Create(ColorCode.None), 0, 10);
            scheme.Add(() => CleanerBox.Create(ColorCode.None), 9, 10);

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Blue, false), 7, 5, 9, 5);
            scheme.Add(() => ColorDoor.Create(ColorCode.Blue, true), 0, 7, 2, 7);
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, true), 7, 6, 9, 6);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, false), 5, 8, 5, 10);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, true), 0, 3, 2, 3);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, true), 7, 4, 9, 4);

            // DDoor

            // Death

            // Sensor
            scheme.Add(() => Sensor.Create(3, 3), 1, 1);
            scheme.Add(() => Sensor.Create(3, 3), 1, 9);
            scheme.Add(() => Sensor.Create(3, 3), 8, 9);
        }
    }
}
