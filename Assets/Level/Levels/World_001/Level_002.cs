using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_002 : Level
    {
        #region Singleton Pattern
        public static Level_002 Instance { get; private set; }
        static Level_002()
        {
            new Level_002();
        }

        private Level_002()
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
            scheme.Add(() => ColorBox.Create(ColorCode.Green), 4, 4);
        }
    }
}
