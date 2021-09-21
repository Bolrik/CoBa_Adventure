using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        #region Singleton Pattern		
        public static AudioManager Instance { get; private set; }

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


        [SerializeField] private AudioSource audioSource;
        public AudioSource AudioSource { get { return audioSource; } }

        int MusicClipIndex { get; set; } = -1;

        public void SetPitch(float value)
        {
            this.AudioSource.pitch = value;
        }

        private void Update()
        {
            if (!this.AudioSource.isPlaying)
            {
                this.MusicClipIndex++;
                this.MusicClipIndex %= AudioAssets.MusicClips.Length;

                this.AudioSource.clip = AudioAssets.MusicClips[this.MusicClipIndex];
                this.AudioSource.Play();
            }
        }
    }
}
