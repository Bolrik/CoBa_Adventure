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
    public class LevelTransitionManager : MonoBehaviour
    {
        #region Singleton Pattern		
        public static LevelTransitionManager Instance { get; private set; }

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

        private void Start()
        {
            LevelManager.Instance.LevelDone += this.StartFadeOut;
            LevelManager.Instance.PreLoadLevel += this.StartFadeIn;
            LevelManager.Instance.PreLoadLevel += this.ClearTransitionText;
        }
        #endregion



        [SerializeField] private GameObject canvas;
        GameObject Canvas { get { return canvas; } }

        [SerializeField] private Animator animator;
        Animator Animator { get { return animator; } }

        [SerializeField] private Text transitionText;
        Text TransitionText { get { return transitionText; } }


        public void StartFadeOut(LevelDoneEventArgs eventArgs)
        {
            this.Canvas.SetActive(true);
            this.Animator.SetTrigger("FadeOut");
            this.AutoClose = false;
        }

        public void StartFadeIn()
        {
            this.Canvas.SetActive(true);
            this.Animator.SetTrigger("FadeIn");
            // this.AutoClose = true;
        }

        bool AutoClose { get; set; }

        private void LateUpdate()
        {
            if (this.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && this.AutoClose)
            {
                this.Canvas.SetActive(false);
            }
        }

        public void SetTransitionText(string text)
        {
            this.TransitionText.text = text;
        }

        public void ClearTransitionText()
        {
            this.TransitionText.text = string.Empty;
        }

        
    }
}