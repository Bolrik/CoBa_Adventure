using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_014 : Level
    {
        #region Singleton Pattern
        public static Level_014 Instance { get; private set; }
        static Level_014()
        {
            new Level_014();
        }

        private Level_014()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 8;
        public override int ScaleY => 11;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 0, 3, 4, 3);
            scheme.Add(() => Wall.Create(), 4, 7, 4, 10);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 6, 9);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.None), 2, 1);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Green), 0, 1);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.Green), 1, 5);

            // Cleaner

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, true), 5, 7, 7, 7);

            // DDoor
            scheme.Add(() => DetectorDoor.Create(ColorCode.None, 2, false), 4, 0, 4, 2);

        }
    }
}
