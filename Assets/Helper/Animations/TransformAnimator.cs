using System;
using System.Linq;
using UnityEngine;

namespace Helper
{
    public class TransformAnimator : AnimatorBase
    {
        [Header("Settings")]
        [SerializeField] private Space space = Space.Self;
        public Space Space { get { return space; } private set { space = value; } }

        [SerializeField] private AnimationMode mode;
        public AnimationMode Mode { get { return mode; } private set { mode = value; } }

        [Header("Ignores")]
        [SerializeField] private bool ignorePosition;
        public bool IgnorePosition { get { return ignorePosition; } private set { ignorePosition = value; } }

        [SerializeField] private bool ignoreRotation;
        public bool IgnoreRotation { get { return ignoreRotation; } private set { ignoreRotation = value; } }

        [SerializeField] private bool ignoreScale;
        public bool IgnoreScale { get { return ignoreScale; } private set { ignoreScale = value; } }

        [Header("Steps")]
        [SerializeField] private TransformAnimatorStep[] animatorSteps;
        public TransformAnimatorStep[] AnimatorSteps { get { return animatorSteps; } private set { animatorSteps = value; } }

        int StepIndex { get; set; }
        float StepTime { get; set; }
        float StepDuration { get; set; }

        bool IsPlaying { get; set; } = true;

        TransformAnimatorStep[] ActiveSteps { get; set; }

        private void Start()
        {
            float startTime = this.AnimatorSteps.Min(step => step.Time);
            float endTime = this.AnimatorSteps.Max(step => step.Time);
            float duration = endTime - startTime;

            this.Restart();
        }

        void Restart()
        {
            this.StepIndex = -1;

            this.NextStep();
        }

        private void Update()
        {
            if (!this.IsPlaying ||
                this.AnimatorSteps.Length <= 1)
            {
                this.Pause();
                return;
            }

            TransformAnimatorStep currentStep = this.ActiveSteps[0];
            TransformAnimatorStep nextStep = this.ActiveSteps[1];

            this.StepTime += Time.deltaTime;
            float done = this.StepTime / this.StepDuration;

            Vector3 pos = Vector3.Lerp(currentStep.Position, nextStep.Position, done);
            Vector3 rot = Vector3.Lerp(currentStep.Rotation, nextStep.Rotation, done);
            Vector3 sca = Vector3.Lerp(currentStep.Scale, nextStep.Scale, done);

            if (this.Space == Space.Self)
            {
                if (!this.IgnorePosition) this.transform.localPosition = pos;
                if (!this.IgnoreRotation) this.transform.localEulerAngles = rot;
                if (!this.IgnoreScale) this.transform.localScale = sca;
            }
            else
            {
                if (!this.IgnorePosition) this.transform.position = pos;
                if (!this.IgnoreRotation) this.transform.eulerAngles = rot;
                if (!this.IgnoreScale) this.transform.localScale = sca;
            }

            if (done >= 1)
            {
                float overflow = this.StepTime - this.StepDuration;
                this.NextStep();
                this.StepTime += overflow;
            }
        }

        private void NextStep()
        {
            int length = this.AnimatorSteps.Length;

            // Adjust Length
            switch (this.Mode)
            {
                case AnimationMode.Once:
                case AnimationMode.Loop:
                    break;
                case AnimationMode.PingPong:
                    length = (length - 1) * 2;
                    break;
                default:
                    throw new NotImplementedException();
            }

            int stepIndex = this.StepIndex;
            stepIndex += 1;

            if (stepIndex >= length - 1)
            {
                switch (this.Mode)
                {
                    case AnimationMode.Once:
                        this.Pause();
                        return;
                    case AnimationMode.Loop:
                    case AnimationMode.PingPong:
                        stepIndex %= length;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            this.StepIndex = stepIndex;
            this.StepTime = 0;

            this.GetSteps(out TransformAnimatorStep currentStep, out TransformAnimatorStep nextStep);

            this.ActiveSteps = new[] { currentStep, nextStep };
            this.StepDuration = Math.Max(nextStep.Time, currentStep.Time) - Math.Min(nextStep.Time, currentStep.Time);

            Debug.Log(this.StepIndex);
        }

        private void GetSteps(out TransformAnimatorStep currentStep, out TransformAnimatorStep nextStep)
        {
            int currentStepIndex = this.StepIndex;
            int nextStepIndex = currentStepIndex + 1;
            string log = $"c: {currentStepIndex} n: {nextStepIndex}";

            switch (this.Mode)
            {
                case AnimationMode.Once:
                case AnimationMode.Loop:
                    break;
                case AnimationMode.PingPong:
                    int length = this.AnimatorSteps.Length - 1;
                    currentStepIndex = currentStepIndex.PingPong(0, length);
                    nextStepIndex = nextStepIndex.PingPong(0, length);
                    break;
                default:
                    break;
            }
            log += $" === C: {currentStepIndex} N: {nextStepIndex}";
            // Debug.Log(log);

            currentStep = this.AnimatorSteps[currentStepIndex];
            nextStep = this.AnimatorSteps[nextStepIndex];
        }

        private void Play()
        {
            this.IsPlaying = true;
        }

        private void Pause()
        {
            this.IsPlaying = false;
        }
    }

    public enum AnimationMode
    {
        Once,
        Loop,
        PingPong
    }

    [System.Serializable]
    public struct TransformAnimatorStep
    {
        [SerializeField] private float time;
        public float Time { get { return time; } private set { time = value; } }

        [SerializeField] private Vector3 position;
        public Vector3 Position { get { return this.position; } private set { this.position = value; } }

        [SerializeField] private Vector3 rotation;
        public Vector3 Rotation { get { return this.rotation; } private set { this.rotation = value; } }

        [SerializeField] private Vector3 scale;
        public Vector3 Scale { get { return scale; } private set { scale = value; } }

        public TransformAnimatorStep(float time, Vector3 position, Vector3 rotation, Vector3 scale) : this()
        {
            this.Time = time;
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
        }
    }
}