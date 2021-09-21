using Basics;
using UnityEngine;

namespace Level
{
    public abstract class Level
    {
        public static string[] LevelDonePhrases { get; } = new[]
            {
                "Well Done!",
                "Good Job!",
                "Keep It up!",
                "Nice!",
                "*GG*!",
                "Amazing!",
                "Awesome!",
                "Keep going!"
            };

        public abstract int ScaleX { get; }
        public abstract int ScaleY { get; }

        public virtual bool IsUnlocked { get; private set; }

        public LevelLayout Load()
        {
            LevelLayoutScheme scheme = new LevelLayoutScheme(this.ScaleX, this.ScaleY);
            this.BuildScheme(scheme);

            LevelLayout layout = new LevelLayout(scheme);

            return layout;
        }
        protected abstract void BuildScheme(LevelLayoutScheme scheme);


        public virtual void GetCameraDefaults(out float x, out float y, out int ppu)
        {
            this.GetCameraDefaults(out x, out y);

            // ppu = 1080 / (this.ScaleY + 2);  //GameCamera.Instance.CurrentPPU;
            // ppu = (int)(1f * currentResolutionHeight * (currentResolutionHeight / 1080) / (this.ScaleY + 2));
            //int currentResolutionHeight = Screen.currentResolution.height;
            //ppu = currentResolutionHeight / (this.ScaleY + 2);
            ppu = Mathf.Clamp(GameCamera.Instance.CameraHeight, 0, 1080) / (this.ScaleY + 4);
        }

        protected virtual void GetCameraDefaults(out float x, out float y)
        {
            x = this.ScaleX / 2f - .5f;
            y = this.ScaleY / 2f - .5f;
        }

        public virtual void PostLoadActions()
        {

        }

        public virtual string GetVictoryText()
        {
            return LevelDonePhrases[UnityEngine.Random.Range(0, LevelDonePhrases.Length)];
        }


        public void SetUnlocked(bool value)
        {
            this.IsUnlocked = value;
        }
    }
}
