using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_023 : Level
    {
        #region Singleton Pattern
        public static Level_023 Instance { get; private set; }
        static Level_023()
        {
            new Level_023();
        }

        private Level_023()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 7;
        public override int ScaleY => 14;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 3, 0, 3, 10);
            scheme.Add(() => Wall.Create(), 4, 10, 6, 10);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 5, 12);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.None), 1, 2);
            scheme.Add(() => ColorBall.Create(ColorCode.None), 5, 1);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Red), 0, 13);
            scheme.Add(() => ColorBox.Create(ColorCode.Green), 5, 9);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.Red), 1, 0);

            // Cleaner
            

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.None, true), 0, 4, 2, 4);
            scheme.Add(() => ColorDoor.Create(ColorCode.None, false), 0, 10, 2, 10);
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, true), 3, 11, 3, 13);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, true), 4, 7, 6, 7);

            // DDoor

            // Death

            // Sensor
            scheme.Add(() => Sensor.Create(3, 3), 5, 4);
            
        }
    }
}
