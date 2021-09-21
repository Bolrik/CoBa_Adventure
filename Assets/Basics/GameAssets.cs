using PlayerInteraction;
using PlayerInteraction.Interactives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Basics
{
    public class GameAssets : MonoBehaviour
    {
        #region Singleton Pattern		
        static GameAssets Instance { get; set; }

        private void Awake()
        {
            if (Instance != null)
            {
                if (Instance == this)
                    return;

                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion


        [Header("Sprites")]
        [SerializeField] private Sprite[] colorBallSprites;
        public static Sprite[] ColorBallSprites { get { return Instance.colorBallSprites; } }

        [SerializeField] private Sprite[] colorDoorSprites;
        public static Sprite[] ColorDoorSprites { get { return Instance.colorDoorSprites; } }

        [SerializeField] private Sprite[] detectorDoorSprites;
        public static Sprite[] DetectorDoorSprites { get { return Instance.detectorDoorSprites; } }

        [SerializeField] private Sprite[] numberSprites;
        public static Sprite[] NumberSprites { get { return Instance.numberSprites; } }

        [SerializeField] private Sprite[] spikeSprites;
        public static Sprite[] SpikeSprites { get { return Instance.spikeSprites; } }

        [SerializeField] private Sprite wallSprite;
        public static Sprite WallSprite { get { return Instance.wallSprite; } }




        [Header("Assets/Basic")]
        [SerializeField] private Ground ground;
        public static Ground Ground { get { return Instance.ground; } }

        [SerializeField] private Wall wall;
        public static Wall Wall { get { return Instance.wall; } }

        [SerializeField] private ColorBall colorBall;
        public static ColorBall ColorBall { get { return Instance.colorBall; } }


        [Header("Assets/Functional")]
        [SerializeField] private ColorBox colorBox;
        public static ColorBox ColorBox { get { return Instance.colorBox; } }

        [SerializeField] private ColorDoor colorDoor;
        public static ColorDoor ColorDoor { get { return Instance.colorDoor; } }

        [SerializeField] private ColorButton colorDetector;
        public static ColorButton ColorDetector { get { return Instance.colorDetector; } }

        [SerializeField] private CleanerBox cleanerBox;
        public static CleanerBox CleanerBox { get { return Instance.cleanerBox; } }

        [SerializeField] private DetectorDoor detectorDoor;
        public static DetectorDoor DetectorDoor { get { return Instance.detectorDoor; } }

        [SerializeField] private GoalFlag goalFlag;
        public static GoalFlag GoalFlag { get { return Instance.goalFlag; } }

        [SerializeField] private DeathBox deathBox;
        public static DeathBox DeathBox { get { return Instance.deathBox; } }

        [SerializeField] private Spike spike;
        public static Spike Spike { get { return Instance.spike; } }

        [SerializeField] private Sensor sensor;
        public static Sensor Sensor { get { return Instance.sensor; } }
    }
}