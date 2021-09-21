using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_020 : Level
    {
        #region Singleton Pattern
        public static Level_020 Instance { get; private set; }
        static Level_020()
        {
            new Level_020();
        }

        private Level_020()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 5;
        public override int ScaleY => 11;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 0, 5, 0, 7);
            scheme.Add(() => Wall.Create(), 4, 5, 4, 7);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 2, 9);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.None), 1, 1);
            scheme.Add(() => ColorBall.Create(ColorCode.None), 3, 1);

            // Box

            // Button

            // Cleaner

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.None, true), 1, 7, 3, 7);

            // DDoor

            // Death

            // Sensor
            scheme.Add(() => Sensor.Create(5, 2), 2, 3.5f);

        }
    }
}
