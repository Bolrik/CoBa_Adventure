using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_004 : Level
    {
        #region Singleton Pattern
        public static Level_004 Instance { get; private set; }
        static Level_004()
        {
            new Level_004();
        }

        private Level_004()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 8;
        public override int ScaleY => 8;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            scheme.Add(() => GoalFlag.Create(), 6, 6);
            scheme.Add(() => ColorBall.Create(ColorCode.None), 1, 1);

            // Buttons
            scheme.Add(() => ColorButton.Create(ColorCode.None), 7, 0);

            // Doors
            scheme.Add(() => ColorDoor.Create(ColorCode.None, true), 2, 4, 5, 4);

            // Walls
            scheme.Add(() => Wall.Create(), 0, 4, 1, 4);
            scheme.Add(() => Wall.Create(), 6, 4, 7, 4);
        }
    }
}
