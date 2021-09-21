using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_019 : Level
    {
        #region Singleton Pattern
        public static Level_019 Instance { get; private set; }
        static Level_019()
        {
            new Level_019();
        }

        private Level_019()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 7;
        public override int ScaleY => 9;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 3, 5, 3, 8);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 5, 7);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.None), 2, 1);
            scheme.Add(() => ColorBall.Create(ColorCode.None), 4, 1);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Green), 6, 1);
            scheme.Add(() => ColorBox.Create(ColorCode.Red), 0, 1);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.Yellow), 1, 7);

            // Cleaner

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Yellow, true), 4, 5, 6, 5);

            // DDoor

            // Death
        }
    }
}
