using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_021 : Level
    {
        #region Singleton Pattern
        public static Level_021 Instance { get; private set; }
        static Level_021()
        {
            new Level_021();
        }

        private Level_021()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 3;
        public override int ScaleY => 12;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall

            // Flag
            scheme.Add(() => GoalFlag.Create(), 1, 11);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.None), 1, 1);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Red), 0, 4);

            // Button

            // Cleaner

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, true), 0, 8, 2, 8);

            // DDoor

            // Death

            // Sensor
            scheme.Add(() => Sensor.Create(3, 4), 1, 3.5f);
        }
    }
}
