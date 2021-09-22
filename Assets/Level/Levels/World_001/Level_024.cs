using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_024 : Level
    {
        #region Singleton Pattern
        public static Level_024 Instance { get; private set; }
        static Level_024()
        {
            new Level_024();
        }

        private Level_024()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 9;
        public override int ScaleY => 11;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 3, 3, 3, 7);
            scheme.Add(() => Wall.Create(), 4, 7, 8, 7);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 6, 5);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.Green), 4, 1);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Blue), 6, 10);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.None), 3, 5);
            scheme.Add(() => ColorButton.Create(ColorCode.Blue), 6, 7);

            // Cleaner
            

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Blue, true), 4, 3, 8, 3);

            // DDoor

            // Death

            // Sensor

            // Spikes
            scheme.Add(() => Spike.Create(ColorCode.Green, true), 0, 7, 2, 7);
        }
    }
}
