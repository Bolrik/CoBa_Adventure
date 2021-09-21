using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_003 : Level
    {
        #region Singleton Pattern
        public static Level_003 Instance { get; private set; }
        static Level_003()
        {
            new Level_003();
        }

        private Level_003()
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

            // Color Box
            scheme.Add(() => ColorBox.Create(ColorCode.Red), 7, 0);
            scheme.Add(() => ColorBox.Create(ColorCode.Green), 4, 4);
            scheme.Add(() => ColorBox.Create(ColorCode.Blue), 0, 7);
        }
    }
}
