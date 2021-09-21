using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_018 : Level
    {
        #region Singleton Pattern
        public static Level_018 Instance { get; private set; }
        static Level_018()
        {
            new Level_018();
        }

        private Level_018()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 12;
        public override int ScaleY => 11;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 11, 0, 11, 0);
            scheme.Add(() => Wall.Create(), 11, 2, 11, 4);
            scheme.Add(() => Wall.Create(), 11, 6, 11, 7);
            scheme.Add(() => Wall.Create(), 4, 0, 4, 0);
            scheme.Add(() => Wall.Create(), 4, 2, 4, 4);
            scheme.Add(() => Wall.Create(), 4, 6, 4, 7);
            scheme.Add(() => Wall.Create(), 5, 3, 7, 3);
            scheme.Add(() => Wall.Create(), 5, 7, 10, 7);
            scheme.Add(() => Wall.Create(), 7, 7, 7, 10);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 5, 9);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.None), 2, 1);
            scheme.Add(() => ColorBall.Create(ColorCode.None), 7, 1);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Green), 11, 5);
            scheme.Add(() => ColorBox.Create(ColorCode.Red), 11, 1);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.Green), 0, 5);
            scheme.Add(() => ColorButton.Create(ColorCode.Red), 0, 1);

            // Cleaner
            scheme.Add(() => CleanerBox.Create(ColorCode.None), 4, 1);
            scheme.Add(() => CleanerBox.Create(ColorCode.None), 4, 5);

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, true), 0, 7, 3, 7);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, true), 0, 3, 3, 3);

            // DDoor

            // Death

        }
    }
}
