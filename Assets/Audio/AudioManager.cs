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

        [SerializeField] private AudioSource musicSource;
        public AudioSource MusicSource { get { return musicSource; } }


        int MusicClipIndex { get; set; } = -1;

        public void SetPitch(float value)
        {
            this.MusicSource.pitch = value;
        }

        private void Update()
        {
            if (!this.MusicSource.isPlaying)
            {
                this.MusicClipIndex++;
                this.MusicClipIndex %= AudioAssets.MusicClips.Length;

                this.MusicSource.clip = AudioAssets.MusicClips[this.MusicClipIndex];
                this.MusicSource.Play();
            }
        }

        public void Play(AudioClip audioClip)
        {
            this.AudioSource.PlayOneShot(audioClip);
        }

        public void PlayRandom(AudioClip[] audioClips)
        {
            AudioClip clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
            this.Play(clip);
        }
    }
}
