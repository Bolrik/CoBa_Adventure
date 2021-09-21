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
    public class ColorModeToggle : MonoBehaviour
    {

        [SerializeField] private GameObject previewRYB;
        public GameObject PreviewRYB { get { return previewRYB; } }

        [SerializeField] private GameObject previewRGB;
        public GameObject PreviewRGB { get { return previewRGB; } }

        [SerializeField] private GameObject previewMCY;
        public GameObject PreviewMCY { get { return previewMCY; } }


        [SerializeField] private Toggle toggleRYB;
        public Toggle ToggleRYB { get { return toggleRYB; } }

        [SerializeField] private Toggle toggleRGB;
        public Toggle ToggleRGB { get { return toggleRGB; } }

        [SerializeField] private Toggle toggleMCY;
        public Toggle ToggleCMY { get { return toggleMCY; } }

        bool IsReady { get; set; }


        private void Start()
        {
            ColorMode colorMode = ColorCodeManager.ColorMode;
            Debug.Log(colorMode);
            
            switch (colorMode)
            {
                case ColorMode.RYB:
                    this.SetPreviewCMY(false);
                    this.SetPreviewRGB(false);
                    this.SetPreviewRYB(true);

                    Debug.Log("RYB");
                    this.ToggleRYB.isOn = true;
                    break;

                case ColorMode.RGB:
                    this.SetPreviewCMY(false);
                    this.SetPreviewRGB(true);
                    this.SetPreviewRYB(false);

                    Debug.Log("RGB");
                    this.ToggleRGB.isOn = true;
                    break;

                case ColorMode.CMYK:
                    this.SetPreviewCMY(true);
                    this.SetPreviewRGB(false);
                    this.SetPreviewRYB(false);

                    Debug.Log("CMY");
                    this.ToggleCMY.isOn = true;
                    break;
            }

            this.IsReady = true;
        }


        public void SelectCMY(bool isActive)
        {
            if (!this.IsReady)
                return;

            if (isActive)
                ColorCodeManager.Instance.SetColorMode(ColorMode.CMYK);

            this.SetPreviewCMY(isActive);
        }
        private void SetPreviewCMY(bool value)
        {
            this.PreviewMCY.SetActive(value);
        }

        public void SelectRGB(bool isActive)
        {
            if (!this.IsReady)
                return;

            if (isActive)
                ColorCodeManager.Instance.SetColorMode(ColorMode.RGB);

            this.SetPreviewRGB(isActive);
        }
        private void SetPreviewRGB(bool value)
        {
            this.PreviewRGB.SetActive(value);
        }

        public void SelectRYB(bool isActive)
        {
            if (!this.IsReady)
                return;

            if (isActive)
                ColorCodeManager.Instance.SetColorMode(ColorMode.RYB);

            this.SetPreviewRYB(isActive);
        }
        private void SetPreviewRYB(bool value)
        {
            this.PreviewRYB.SetActive(value);
        }
    }
}