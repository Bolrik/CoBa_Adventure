using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_006 : Level
    {
        #region Singleton Pattern
        public static Level_006 Instance { get; private set; }
        static Level_006()
        {
            new Level_006();
        }

        private Level_006()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 11;
        public override int ScaleY => 7;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            //Walls
            scheme.Add(() => Wall.Create(), 6, 0, 6, 1);
            scheme.Add(() => Wall.Create(), 6, 5, 6, 6);
            scheme.Add(() => Wall.Create(), 7, 0, 7, 1);
            scheme.Add(() => Wall.Create(), 7, 5, 7, 6);

            // Flag
            scheme.Add(() => GoalFlag.Create(), 9, 3);

            // Ball
            scheme.Add(() => ColorBall.Create(ColorCode.None), 3, 3);

            // Box
            scheme.Add(() => ColorBox.Create(ColorCode.Red), 0, 6);

            // Button
            scheme.Add(() => ColorButton.Create(ColorCode.Red), 0, 0);
            scheme.Add(() => ColorButton.Create(ColorCode.None), 5, 0);

            // Door
            scheme.Add(() => ColorDoor.Create(ColorCode.None, true), 6, 2, 6, 4);
            scheme.Add(() => ColorDoor.Create(ColorCode.Red, true), 7, 2, 7, 4);
        }
    }
}
