using Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogs
{
    public class DialogManager : MonoBehaviour
    {
        #region Singleton Pattern		
        public static DialogManager Instance { get; private set; }

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
            this.StartInit();
        }
        #endregion

        [Header("Settings")]
        [SerializeField] private GameObject dialogCanvas;
        public GameObject DialogCanvas { get { return dialogCanvas; } }

        [Header("Settings/Dialog")]
        [SerializeField] private float nextNodeLineIncrementBase = .2f;
        public float NextNodeLineIncrementBase { get { return nextNodeLineIncrementBase; } }


        [Header("Boxes")]
        [SerializeField] private Text dialogBox;
        public Text DialogBox { get { return dialogBox; } }

        [SerializeField] private Text dialogSpeaker;
        public Text DialogSpeaker { get { return dialogSpeaker; } }


        Dialog ActiveDialog { get; set; }
        int NodeIndex { get; set; }
        int NodeLineIndex { get; set; }

        float NextNodeLineIncrement { get; set; }
        // float NextNodeLineIncrementBase { get; set; }

        string DialogNodeText { get; set; }
        System.Random Random { get; set; } = new System.Random();

        bool AwaitResponse { get; set; }


        private void StartInit()
        {
            LevelManager.Instance.PostLoadLevel += this.StartLevelDialog;
            LevelManager.Instance.LevelDone += this.StopDialog;
        }

        private void StartLevelDialog()
        {
            this.DeActivate(false);

            Dialog dialog = DialogAssets.Dialogs.FirstOrDefault(dialog => 
                dialog.LevelIndex == LevelManager.Instance.LevelIndex &&
                dialog.WorldIndex == LevelManager.Instance.WorldIndex);

            if (dialog == null)
                return;

            this.StartDialog(dialog);
        }

        public void StartDialog(Dialog dialog)
        {
            if (this.ActiveDialog != null)
                return;

            this.ActiveDialog = dialog;
            

            this.ResetDialogState();

            this.ReadDialogNodeText();
            this.DeActivate(true);
        }

        private void ResetDialogState()
        {
            this.NodeIndex = 0;
            this.ResetNodeState();
        }

        private void ResetNodeState()
        {
            this.AwaitResponse = false;
            this.NextNodeLineIncrement = this.NodeLineIndex = 0;
        }

        private void Update()
        {
            if (this.ActiveDialog == null)
            {
                return;
            }

            if (this.AwaitResponse)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    this.AwaitResponse = false;

                    if (!this.ReadNextDialogNodeText())
                        this.DeActivate(false);

                    this.ResetNodeState();
                }
            }
            else
                this.AdvanceText();
        }

        private void AdvanceText()
        {
            if (this.NextNodeLineIncrement > 0)
            {
                this.NextNodeLineIncrement -= Time.deltaTime;
                return;
            }

            this.NextNodeLineIncrement = this.NextNodeLineIncrementBase + 
                ((float)(this.Random.NextDouble() * .2 - .1)) * this.NextNodeLineIncrementBase;

            this.NodeLineIndex++;

            if (Input.GetKey(KeyCode.Return))
                this.NodeLineIndex += 6;

            this.NodeLineIndex = Mathf.Clamp(this.NodeLineIndex, 0, this.DialogNodeText.Length);

            this.DialogBox.text = this.DialogNodeText.Substring(0, this.NodeLineIndex);

            if (this.NodeLineIndex >= this.DialogNodeText.Length)
                this.AwaitResponse = true;
        }

        private void ReadDialogNodeText()
        {
            DialogNode activeNode = this.ActiveDialog.DialogNodes[this.NodeIndex];
            
            this.DialogNodeText = activeNode
                .Lines.Aggregate(Environment.NewLine, (a) => a);

            this.DialogSpeaker.text = activeNode.Speaker;
        }

        private bool ReadNextDialogNodeText()
        {
            this.NodeIndex++;

            if (this.NodeIndex >= this.ActiveDialog.DialogNodes.Length)
                return false;

            this.ReadDialogNodeText();

            return true;
        }

        private void DeActivate(bool value)
        {
            if (!value)
                this.ActiveDialog = null;

            this.DialogCanvas.SetActive(value);
        }

        private void StopDialog(LevelDoneEventArgs eventArgs)
        {
            this.DeActivate(false);
        }
    }
}
