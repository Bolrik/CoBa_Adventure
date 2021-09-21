using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_008 : Level
    {
        #region Singleton Pattern
        public static Level_008 Instance { get; private set; }
        static Level_008()
        {
            new Level_008();
        }

        private Level_008()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 14;
        public override int ScaleY => 9;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Wall
            scheme.Add(() => Wall.Create(), 10, 0, 10, 2);
            scheme.Add(() => Wall.Create(), 10, 6, 10, 8);
            scheme.Add(() => Wall.Create(), 5, 0, 5, 2);
            scheme.Add(() => Wall.Create(), 5, 6, 5, 8);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 12, 5);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.None), 3, 5);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Red), 0, 8);
            scheme.Add(() => ColorBox.Create(ColorCode.Green), 6, 8);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.Red), 0, 0);
            scheme.Add(() => ColorButton.Create(ColorCode.Green), 6, 0);

            // Cleaner
            scheme.Add(() => CleanerBox.Create(ColorCode.None), 9, 0);

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, true), 5, 3, 5, 5);
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, true), 10, 3, 10, 5);
        }

        public override void PostLoadActions()
        {
            GameCamera.Instance.Offset(-10, 0);
        }

    }
}
