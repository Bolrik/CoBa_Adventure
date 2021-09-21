using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_010 : Level
    {
        #region Singleton Pattern
        public static Level_010 Instance { get; private set; }
        static Level_010()
        {
            new Level_010();
        }

        private Level_010()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 9;
        public override int ScaleY => 9;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {            
            // Wall
            scheme.Add(() => Wall.Create(), 0, 4, 0, 4);
            scheme.Add(() => Wall.Create(), 3, 4, 5, 4);
            scheme.Add(() => Wall.Create(), 4, 5, 4, 0);
            scheme.Add(() => Wall.Create(), 4, 8, 4, 8);
            scheme.Add(() => Wall.Create(), 8, 4, 8, 4);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 7, 1);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.Green), 1, 1);

            // Box

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.Green), 0, 8);
            scheme.Add(() => ColorButton.Create(ColorCode.None), 0, 0);

            // Cleaner
            scheme.Add(() => CleanerBox.Create(ColorCode.None), 3, 3);

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, false), 1, 4, 2, 4);
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, true), 4, 7, 4, 6);
            scheme.Add(() => ColorDoor.Create(ColorCode.None, true), 6, 4, 7, 4);
        }
    }
}
