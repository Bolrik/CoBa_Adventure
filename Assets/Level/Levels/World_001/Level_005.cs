using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_005 : Level
    {
        #region Singleton Pattern
        public static Level_005 Instance { get; private set; }
        static Level_005()
        {
            new Level_005();
        }

        private Level_005()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 9;
        public override int ScaleY => 8;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            // Walls
            scheme.Add(() => Wall.Create(), 4, 0, 4, 5);

            // Flags
            scheme.Add(() => GoalFlag.Create(), 7, 1);

            // Balls
            scheme.Add(() => ColorBall.Create(ColorCode.None), 1, 1);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Green), 0, 7);

            // Buttons
            scheme.Add(() => ColorButton.Create(ColorCode.Green), 0, 0);

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.Green, true), 5, 3, 8, 3);
        }
    }
}
