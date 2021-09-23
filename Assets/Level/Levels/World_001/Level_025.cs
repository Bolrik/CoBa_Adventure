using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_025 : Level
    {
        #region Singleton Pattern
        public static Level_025 Instance { get; private set; }
        static Level_025()
        {
            new Level_025();
        }

        private Level_025()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 15;
        public override int ScaleY => 12;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 3, 3, 3, 11);
            scheme.Add(() => Wall.Create(), 7, 3, 7, 11);
            scheme.Add(() => Wall.Create(), 11, 3, 11, 11);
            scheme.Add(() => Wall.Create(), 4, 4, 6, 4);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 13, 10);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.Red), 1, 1);
            scheme.Add(() => ColorBall.Create(ColorCode.None), 5, 10);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Red), 4, 3);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.None), 4, 4);

            // Cleaner
            scheme.Add(() => CleanerBox.Create(ColorCode.None), 3, 10);
            scheme.Add(() => CleanerBox.Create(ColorCode.None), 7, 10);
            scheme.Add(() => CleanerBox.Create(ColorCode.None), 6, 4);
            scheme.Add(() => CleanerBox.Create(ColorCode.Green), 11, 10);


            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, true), 3, 0, 3, 2);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, false), 7, 0, 7, 2);
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, true), 11, 0, 11, 2);

            // DDoor
            scheme.Add(() => DetectorDoor.Create(ColorCode.Red, 2, false), 0, 4, 2, 4);
            scheme.Add(() => DetectorDoor.Create(ColorCode.Red, 1, true), 0, 8, 2, 8);
            scheme.Add(() => DetectorDoor.Create(ColorCode.Red, 2, false), 8, 4, 10, 4);

            // Death

            // Sensor
            scheme.Add(() => Sensor.Create(3, 1), 1, 3);
            scheme.Add(() => Sensor.Create(3, 1), 9, 8);
            scheme.Add(() => Sensor.Create(2, 3), 10.5f, 1);

            // Spikes
            scheme.Add(() => Spike.Create(ColorCode.Green, false), 8, 7, 10, 7);
        }
    }
}
