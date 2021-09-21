using Audio;
using Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Basics
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton Pattern		
        public static GameManager Instance { get; private set; }

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


        [SerializeField] private GameObject levelInfoCanvas;
        public GameObject LevelInfoCanvas { get { return levelInfoCanvas; } }

        [SerializeField] private Text levelNumberText;
        public Text LevelNumberText { get { return levelNumberText; } }

        [SerializeField] private AudioSource musicSource;
        public AudioSource MusicSource { get { return musicSource; } }






        GameSpeedManager GameSpeedManager { get => GameSpeedManager.Instance; }

        private void Start()
        {
            SceneManager.LoadScene(1);
            LevelManager.Instance.PostLoadLevel += this.UpdateLevelNumberText;
        }

        private void UpdateLevelNumberText()
        {
            int levelIndex = LevelManager.Instance.LevelIndex;
            int worldIndex = LevelManager.Instance.WorldIndex;

            if (levelIndex < 0)
            {
                this.LevelInfoCanvas.SetActive(false);
            }
            else
            {
                this.LevelInfoCanvas.SetActive(true);
                this.LevelNumberText.text = $"{worldIndex + 1} - {levelIndex + 1}";
            }
        }

        private void Update()
        {
            float gameSpeed = this.GameSpeedManager.UpdateGameSpeed();

            // Decrease pitch to support Slow Motion Effect!
            AudioManager.Instance.SetPitch(1 - (.2f * (1 - gameSpeed)));

            // Input for Level Manager...
            if (SceneManager.GetActiveScene().buildIndex == 2 && 
                Input.GetKeyDown(KeyCode.R))
            {
                LevelManager.Instance.Reload();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                    LevelManager.Instance.BackToTitleScreen();
#warning DEBUG
                else if (Input.GetKeyDown(KeyCode.T))
                    LevelManager.Instance.Next();
                else if (LevelManager.Instance.IsLevelDone &&
                    Input.anyKeyDown)
                    LevelManager.Instance.Next();
            }
        }
    }
}