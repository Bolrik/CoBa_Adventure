using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_009 : Level
    {
        #region Singleton Pattern
        public static Level_009 Instance { get; private set; }
        static Level_009()
        {
            new Level_009();
        }

        private Level_009()
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
            scheme.Add(() => Wall.Create(), 3, 4, 8, 4);
            scheme.Add(() => Wall.Create(), 4, 8, 4, 8);
            scheme.Add(() => Wall.Create(), 4, 5, 4, 3);
            scheme.Add(() => Wall.Create(), 4, 0, 4, 0);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 7, 1);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.Red), 1, 1);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Red), 0, 5);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.None), 0, 8);
            scheme.Add(() => ColorButton.Create(ColorCode.None), 8, 8);

            // Cleaner

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, false), 1, 4, 2, 4);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, true), 4, 7, 4, 6);
            scheme.Add(() => ColorDoor.Create(ColorCode.None, true), 4, 1, 4, 2);
        }
    }
}
