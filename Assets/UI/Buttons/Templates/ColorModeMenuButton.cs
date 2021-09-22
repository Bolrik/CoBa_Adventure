using Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class ColorModeMenuButton : GameButton
    {
        [SerializeField] private GameObject previewRYB;
        public GameObject PreviewRYB { get { return previewRYB; } }

        [SerializeField] private GameObject previewRGB;
        public GameObject PreviewRGB { get { return previewRGB; } }

        [SerializeField] private GameObject previewMCY;
        public GameObject PreviewMCY { get { return previewMCY; } }

        [SerializeField] private Text colorModeNameText;
        public Text ColorModeNameText { get { return colorModeNameText; } }



        private void Start()
        {
            this.UpdatePreview();
        }

        private void UpdatePreview()
        {
            ColorMode colorMode = ColorCodeManager.ColorMode;

            switch (colorMode)
            {
                case ColorMode.RYB:
                    this.SetPreviewCMY(false);
                    this.SetPreviewRGB(false);
                    this.SetPreviewRYB(true);
                    break;

                case ColorMode.RGB:
                    this.SetPreviewCMY(false);
                    this.SetPreviewRGB(true);
                    this.SetPreviewRYB(false);
                    break;

                case ColorMode.CMYK:
                    this.SetPreviewCMY(true);
                    this.SetPreviewRGB(false);
                    this.SetPreviewRYB(false);
                    break;
            }


            this.ColorModeNameText.text = $"{colorMode}";
        }

        private void SetPreviewCMY(bool value)
        {
            this.PreviewMCY.SetActive(value);
        }

        private void SetPreviewRGB(bool value)
        {
            this.PreviewRGB.SetActive(value);
        }

        private void SetPreviewRYB(bool value)
        {
            this.PreviewRYB.SetActive(value);
        }

        public override void OnClick()
        {
            ColorMode colorMode = ColorCodeManager.ColorMode;
            Debug.Log(colorMode);
            colorMode = (ColorMode)(((int)colorMode + 1) % 3);
            Debug.Log(colorMode);
            ColorCodeManager.Instance.SetColorMode(colorMode);

            this.UpdatePreview();
        }
    }
}