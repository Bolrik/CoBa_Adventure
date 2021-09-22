using Basics;
using Helper;
using Level;
using SlingMechanic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerInteraction.Interactives
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        public virtual bool TriggerReaction { get => true; }
        public virtual bool PlayTouchSound { get => true; }

        public void Touch(ColorBall colorBall)
        {
            if (this.PlayTouchSound)
                Audio.AudioManager.Instance.PlayRandom(Audio.AudioAssets.WallHit);

            this.OnTouch(colorBall);
        }
        protected virtual void OnTouch(ColorBall colorBall) { }

        private void OnDrawGizmos()
        {
            this.Visualize();
        }
    }
}