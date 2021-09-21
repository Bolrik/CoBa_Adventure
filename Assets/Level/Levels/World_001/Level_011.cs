using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_011 : Level
    {
        #region Singleton Pattern
        public static Level_011 Instance { get; private set; }
        static Level_011()
        {
            new Level_011();
        }

        private Level_011()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 11;
        public override int ScaleY => 8;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 0, 7, 3, 7);
            scheme.Add(() => Wall.Create(), 3, 3, 9, 3);
            scheme.Add(() => Wall.Create(), 5, 7, 5, 7);
            scheme.Add(() => Wall.Create(), 7, 7, 9, 7);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 10, 1);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.Green), 1, 3);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Blue), 10, 3);
            scheme.Add(() => ColorBox.Create(ColorCode.Green), 0, 6);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.None), 10, 7);
            scheme.Add(() => ColorButton.Create(ColorCode.None), 6, 7);

            // Cleaner
            scheme.Add(() => CleanerBox.Create(ColorCode.None), 4, 7);

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Blue, true), 3, 0, 3, 2);
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, false), 3, 4, 3, 6);
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, true), 7, 4, 7, 6);
        }
    }
}
