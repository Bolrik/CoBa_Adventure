using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_026 : Level
    {
        #region Singleton Pattern
        public static Level_026 Instance { get; private set; }
        static Level_026()
        {
            new Level_026();
        }

        private Level_026()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 13;
        public override int ScaleY => 12;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 5, 3, 5, 8);
            scheme.Add(() => Wall.Create(), 9, 0, 9, 11);
            scheme.Add(() => Wall.Create(), 6, 4, 7, 4);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 11, 10);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.None), 1, 1);
            scheme.Add(() => ColorBall.Create(ColorCode.None), 3, 1);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Red), 7, 3);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.None), 5, 6);

            // Cleaner

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.None, false, true), 0, 4, 4, 4);
            scheme.Add(() => ColorDoor.Create(ColorCode.None, true), 5, 0, 5, 2);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, true), 9, 3, 9, 5);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, true), 8, 4, 8, 4);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, false), 10, 6, 12, 6);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, false, true), 10, 2, 12, 2);

            // DDoor

            // Death
            scheme.Add(() => DeathBox.Create(), 10, 0, 12, 0);

            // Sensor

            // Spikes
            scheme.Add(() => Spike.Create(ColorCode.Red, true), 0, 8, 4, 8);
        }
    }
}
