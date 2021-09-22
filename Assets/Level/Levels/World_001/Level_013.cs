using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_013 : Level
    {
        #region Singleton Pattern
        public static Level_013 Instance { get; private set; }
        static Level_013()
        {
            new Level_013();
        }

        private Level_013()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 7;
        public override int ScaleY => 15;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 3, 11, 3, 14);
            scheme.Add(() => Wall.Create(), 3, 3, 6, 3);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 5, 1);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.None), 1, 1);

            // Box

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.None), 5, 13);

            // Cleaner

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.None, true), 3, 0, 3, 2);

            // DDoor
            scheme.Add(() => DetectorDoor.Create(ColorCode.None, 5, false), 4, 11, 6, 11);
        }
    }
}
