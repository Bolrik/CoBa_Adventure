using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;

namespace Level
{
    public class Level_002_001 : Level
    {
        #region Singleton Pattern
        public static Level_002_001 Instance { get; private set; }
        static Level_002_001()
        {
            new Level_002_001();
        }

        private Level_002_001()
        {
            Instance = this;
        }
        #endregion

        public override int ScaleX => 8;
        public override int ScaleY => 8;

        protected override void BuildScheme(LevelLayoutScheme scheme)
        {
            scheme.Add(() => GoalFlag.Create(), 7, 7);
            scheme.Add(() => GoalFlag.Create(0, 17), 0, 7);

            scheme.Add(() => Spike.Create(ColorCode.Black, true), 2, 0, 2, 7);
            scheme.Add(() => Spike.Create(ColorCode.Black, true), 4, 0, 4, 7);

            scheme.Add(() => ColorDoor.Create(ColorCode.Black, true), 3, 0, 3, 7);

            scheme.Add(() => DeathBox.Create(), 5, 0, 5, 7);

            scheme.Add(() => ColorBall.Create(ColorCode.None), 0, 0);
        }
    }
}
