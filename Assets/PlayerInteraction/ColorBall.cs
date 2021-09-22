using Achievements;
using Basics;
using PlayerInteraction.Interactives;
using SlingMechanic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerInteraction
{
    public class ColorBall : GrabObject, IColorGrabObject
    {
        public static ColorBall Create(ColorCode colorCode)
        {
            ColorBall instance = GameObject.Instantiate(GameAssets.ColorBall);
            instance.ColorCode = colorCode;

            return instance;
        }

        [Header("Visuals")]
        [SerializeField] private SpriteRenderer bodyRenderer;
        public SpriteRenderer BodyRenderer { get { return bodyRenderer; } private set { bodyRenderer = value; } }

        [SerializeField] private SpriteRenderer eyeRenderer;
        public SpriteRenderer EyeRenderer { get { return eyeRenderer; } private set { eyeRenderer = value; } }

        [SerializeField] private TrailRenderer trail;
        public TrailRenderer Trail { get { return trail; } private set { trail = value; } }

        [SerializeField] private ParticleSystem splashParticleSystem;
        public ParticleSystem SplashParticleSystem { get { return splashParticleSystem; } }


        [Header("Grab Object Settings")]
        [SerializeField] private CircleCollider2D grabCollider;
        public CircleCollider2D GrabCollider { get { return grabCollider; } private set { grabCollider = value; } }

        [Header("Info")]
        [SerializeField] private ColorCode colorCode;
        public ColorCode ColorCode { get { return colorCode; } private set { colorCode = value; } }

        Dictionary<Collider2D, InteractiveObject> NearbyInteractiveObjects { get; set; } = 
            new Dictionary<Collider2D, InteractiveObject>();

        public ColorBallInfo ColorBallInfo { get; private set; }


        protected override void OnAwake()
        {
            this.ColorBallInfo = new ColorBallInfo(this);
            this.Trail.endColor = Color.gray;

            base.OnAwake();
        }

        protected override void OnStart()
        {
            base.OnStart();

            this.SetIsAlive(true);
        }


        private void Update()
        {
            this.ColorBallInfo.Update();

            this.Trail.startColor = this.ColorCode.GetColor();
            this.BodyRenderer.color = this.ColorCode.GetColor();

            this.UpdateFace();
        }


        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!(collider.GetComponentInParent<InteractiveObject>() is InteractiveObject interactive))
            {
                return;
            }

            this.NearbyInteractiveObjects.Add(collider, interactive);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            this.NearbyInteractiveObjects.Remove(collider);
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (this.ColorBallInfo.IsAlive)
                Audio.AudioManager.Instance.PlayRandom(Audio.AudioAssets.CoBaHit);

            var splashEffectTrail = this.SplashParticleSystem.trails;
            splashEffectTrail.colorOverTrail = new ParticleSystem.MinMaxGradient(this.ColorCode.GetColor());

            this.SplashParticleSystem.Play();

            if (!(collision.collider.GetComponentInParent<InteractiveObject>() is InteractiveObject interactive))
            {
                return;
            }

            interactive.Touch(this);
        }



        private void UpdateFace()
        {
            if (!this.ColorBallInfo.IsAlive)
            {
                this.EyeRenderer.sprite = GameAssets.ColorBallSprites[3];
                return;
            }

            int spriteIdx = this.ColorBallInfo.IsMoving ? 1 : 0;

            foreach (var nearby in this.NearbyInteractiveObjects)
            {
                if (!nearby.Value.TriggerReaction)
                    continue;

                Collider2D collider = nearby.Key;

                float distance = (this.transform.position - collider.ClosestPoint(this.transform.position).ToVector3()).magnitude;

                if (distance < GameSettings.SlowMotionPeak && this.ColorBallInfo.IsAwake && this.ColorBallInfo.IsMoving)
                    spriteIdx = 2;
            }

            this.EyeRenderer.sprite = GameAssets.ColorBallSprites[spriteIdx];
        }

        
        public float GetDesiredGameSpeed()
        {
            if (!this.ColorBallInfo.IsAlive)
                return 1;

            if (!(this.ColorBallInfo.IsAwake && this.ColorBallInfo.IsMoving))
                return 1;

            float desiredGameSpeed = 1;

            foreach (var interactivePair in this.NearbyInteractiveObjects)
            {
                if (!interactivePair.Value.TriggerReaction)
                    continue;

                if (interactivePair.Value is ColorBox colorBox &&
                    (this.ColorCode == ColorCode.None || colorBox.ColorCode == this.ColorCode))
                    continue;

                if (interactivePair.Value is Spike spike &&
                    !spike.IsActive)
                    continue;

                Collider2D collider = interactivePair.Key;

                float distance = (this.transform.position - collider.ClosestPoint(this.transform.position).ToVector3()).magnitude;
                float distanceAdjusted = (distance - GameSettings.SlowMotionPeak);
                float percent = distanceAdjusted / (GameSettings.SlowMotionRange);

                if (percent < desiredGameSpeed)
                    desiredGameSpeed = percent;
            }

            return desiredGameSpeed;
        }


        public override bool IsGrabbableFromPosition(Vector3 worldPosition)
        {
            return this.GrabCollider.OverlapPoint(worldPosition);
        }



        public void Die()
        {
            this.SetIsAlive(false);
            // ColorBallManager.Instance.Unregister(this);
        }

        public void Revive()
        {
            this.SetIsAlive(true);
            // ColorBallManager.Instance.Register(this);
        }

        private void SetIsAlive(bool value)
        {
            if (this.ColorBallInfo.IsAlive == value)
                return;

            this.ColorBallInfo.SetIsAlive(value);
            this.SetIsGrabbable(this.ColorBallInfo.IsAlive);

            if (value)
            {
                ColorBallManager.Instance.Register(this);
            }
            else
            {
                ColorBallManager.Instance.Unregister(this);
            }
        }


        // Color
        public void AddColorCode(ColorCode colorCode)
        {
            //if (!this.ColorBallInfo.IsAlive)
            //    return;

            this.SetColorCode(this.ColorCode.Mix(colorCode));
        }

        public void SetColorCode(ColorCode colorCode)
        {
            //if (!this.ColorBallInfo.IsAlive)
            //    return;

            this.ColorCode = colorCode;
            
            if (this.ColorCode > ColorCode.Blue && this.ColorBallInfo.IsAlive)
            {
                this.Die();
            }
        }


        // Interface, IColorGrabObject
        ColorCode IColorGrabObject.GetColorCode() => this.ColorCode;

        public override void OnGrab()
        {
            AchievementManager.Instance.Set(AchievementType.GrabACoBa);

            Audio.AudioManager.Instance.PlayRandom(Audio.AudioAssets.CoBaSelect);
        }
    }

    public class ColorBallInfo
    {
        public ColorBall ColorBall { get; private set; }

        public bool IsMoving { get; private set; }
        public bool IsAwake { get; private set; }
        public bool IsAlive { get; private set; }

        public ColorBallInfo(ColorBall colorBall)
        {
            this.ColorBall = colorBall;
        }

        public void Update()
        {
            this.IsAwake = this.ColorBall.Body.IsAwake();
            this.IsMoving = this.ColorBall.Body.velocity.sqrMagnitude > .1f;
        }

        public void SetIsAlive(bool value) => this.IsAlive = value;
    }
}