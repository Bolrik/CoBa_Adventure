using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_001 : Level
    {
        #region Singleton Pattern
        public static Level_001 Instance { get; private set; }
        static Level_001()
        {
            new Level_001();
        }

        private Level_001()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 8;
        public override int ScaleY => 8;

        public override bool IsUnlocked => true;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            scheme.Add(() => GoalFlag.Create(), 6, 6);
            scheme.Add(() => ColorBall.Create(ColorCode.None), 1, 1);
        }
    }
}
