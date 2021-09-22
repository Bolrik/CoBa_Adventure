using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Audio
{
    public class AudioAssets : MonoBehaviour
    {
        #region Singleton Pattern		
        static AudioAssets Instance { get; set; }

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


        [SerializeField] private AudioClip[] musicClips;
        public static AudioClip[] MusicClips { get { return Instance.musicClips; } }


        [SerializeField] private AudioClip[] wallHit;
        public static AudioClip[] WallHit { get { return Instance.wallHit; } }

        [SerializeField] private AudioClip[] coBaHit;
        public static AudioClip[] CoBaHit { get { return Instance.coBaHit; } }

        [SerializeField] private AudioClip[] coBaSelect;
        public static AudioClip[] CoBaSelect { get { return Instance.coBaSelect; } }

        [SerializeField] private AudioClip buttonHover;
        public static AudioClip ButtonHover { get { return Instance.buttonHover; } }


    }
}
