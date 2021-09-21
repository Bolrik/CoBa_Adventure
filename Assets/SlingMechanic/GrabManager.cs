using Basics;
using Level;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlingMechanic
{
    public class GrabManager : MonoBehaviour
    {
        #region Singleton Pattern		
        public static GrabManager Instance { get; private set; }

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

            this.Initialize();
        }
        #endregion

        private void Initialize()
        {
            LevelManager.Instance.PreLoadLevel += this.ReInitialize;
        }


        [Header("Visuals")]
        [SerializeField] private LineRenderer grabLine;
        public LineRenderer GrabLine { get { return grabLine; } private set { grabLine = value; } }

        [SerializeField] private LineRenderer slingPreviewLine;
        public LineRenderer SlingPreviewLine { get { return slingPreviewLine; } private set { slingPreviewLine = value; } }

        [Header("Info")]
        [SerializeField] private IGrabObject atCursor;
        public IGrabObject AtCursor { get { return atCursor; } set { atCursor = value; } }

        [Header("Config")]
        [SerializeField] private float slingMultiplier = 1.5f;
        public float SlingMultiplier { get { return slingMultiplier; } private set { slingMultiplier = value; } }

        private IGrabObject CurrentGrabObject { get; set; }
        Vector2 GrabDelta { get; set; }


        List<IGrabObject> GrabbableObjects { get; set; } = new List<IGrabObject>();
        List<IGrabSignalReceiver> Receivers { get; set; } = new List<IGrabSignalReceiver>();

        // Calculated Properties
        bool IsGrabbing { get => this.CurrentGrabObject != null; }

        // Instance Properties
        GameCamera GameCamera { get => GameCamera.Instance; }

        // "Events"
        public Action<IGrabObject> GrabStart { get; set; }
        public Action<IGrabObject> GrabReleased { get; set; }


        public void Register(GrabObject grabObject)
        {
            this.GrabbableObjects.Add(grabObject);
        }

        public void Unregister(GrabObject grabObject)
        {
            this.GrabbableObjects.Remove(grabObject);
        }

        public void Register(IGrabSignalReceiver signalReceiver)
        {
            this.Receivers.Add(signalReceiver);
        }
        public void Unregister(IGrabSignalReceiver signalReceiver)
        {
            this.Receivers.Remove(signalReceiver);
        }


        public void Update()
        {
            this.UpdateAtCursor();
            this.TryGrabObject();
            this.HandleDrag();

            this.UpdateGrabVisuals();
        }

        private void UpdateGrabVisuals()
        {
            if (this.CurrentGrabObject == null)
            {
                // Disable Indicator
                this.GrabLine.enabled = false;
                this.SlingPreviewLine.enabled = false;
            }
            else
            {
                this.GrabLine.enabled = true;
                this.SlingPreviewLine.enabled = true;
            }
        }

        private void UpdateAtCursor()
        {
            var worldPos = this.GameCamera.GetWorldPosition();

            foreach (var grabObject in this.GrabbableObjects)
            {
                if (grabObject.IsGrabbableFromPosition(worldPos))
                {
                    this.AtCursor = grabObject;
                    return;
                }
            }

            this.AtCursor = null;
        }

        private void TryGrabObject()
        {
            if (this.AtCursor == null ||
                this.AtCursor.GrabType == GrabType.None ||
                this.AtCursor.IsGrabbable == false)
                return;

            if (!this.IsGrabbing && Input.GetMouseButton(0))
            {
                this.Grab(this.AtCursor);
                return;
            }
        }

        private void Grab(IGrabObject grabObject)
        {
            this.CurrentGrabObject = grabObject;
            this.CurrentGrabObject.OnGrab();
            this.GrabStart?.Invoke(this.CurrentGrabObject);
        }

        private void Release()
        {
            this.SendGrabSignal();
            this.GrabReleased?.Invoke(this.CurrentGrabObject);

            this.CurrentGrabObject = null;
        }

        private void HandleDrag()
        {
            if (this.CurrentGrabObject == null ||
                this.CurrentGrabObject.GrabType == GrabType.None)
                return;

            this.CalculateGrabDelta();
            this.HandleGrabDelta();
        }

        private void CalculateGrabDelta()
        {
            Vector2 worldPosition = this.GameCamera.GetWorldPosition();
            this.GrabDelta = worldPosition.Offset(-this.CurrentGrabObject.transform.position);

            Vector3 worldPosition3 = worldPosition.ToVector3();

            Vector3 worldCurrentDelta = worldPosition3 - this.CurrentGrabObject.transform.position;
            Vector3 slingPreviewTarget = this.CurrentGrabObject.transform.position - (worldCurrentDelta * this.SlingMultiplier);

            if (this.CurrentGrabObject is IColorGrabObject colorGrabObject)
                this.GrabLine.startColor = colorGrabObject.GetColorCode().GetColor();
            else
                this.GrabLine.startColor = Color.white;

            this.GrabLine.SetPositions(new[]
            {
                this.CurrentGrabObject.transform.position,
                Vector3.Lerp(this.CurrentGrabObject.transform.position, worldPosition3, .75f),
                worldPosition3
            });
            
            this.SlingPreviewLine.SetPositions(new[]
            {
                this.CurrentGrabObject.transform.position,
                slingPreviewTarget
            });
        }

        private void HandleGrabDelta()
        {
            if (!this.CurrentGrabObject.IsGrabbable)
            {
                this.Release();
                return;
            }

            float strength = Mathf.Clamp(this.CurrentGrabObject.Body.drag, 1, this.CurrentGrabObject.Body.drag);

            if (this.CurrentGrabObject.GrabType == GrabType.Drag)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    this.Release();
                    return;
                }

                if (this.GrabDelta.magnitude > 1)
                {
                    this.CurrentGrabObject.Body.AddForce(this.GrabDelta * strength);
                    this.CurrentGrabObject.OnDrag();
                }
            }
            else if (this.CurrentGrabObject.GrabType == GrabType.Sling)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Vector2 direction = this.GrabDelta.normalized;
                    
                    // Limit Sling Power?? NOPE
                    //float power = Mathf.Clamp(this.GrabDelta.magnitude, 0, 1);
                    float power = this.GrabDelta.magnitude;

                    this.CurrentGrabObject.Body.AddForce(direction * power * -strength * this.SlingMultiplier, ForceMode2D.Impulse);

                    //this.CurrentGrabObject.Body.AddForce(this.GrabDelta * -strength * this.SlingMultiplier, ForceMode2D.Impulse);
                    this.CurrentGrabObject.OnSling();

                    this.Release();
                    return;
                }
            }
        }

        private void SendGrabSignal()
        {
            foreach (var signalReceiver in this.Receivers)
            {
                signalReceiver.UpdateSignal(this.CurrentGrabObject);
            }
        }

        private void ReInitialize()
        {
            this.GrabbableObjects.Clear();
            this.Receivers.Clear();
            this.CurrentGrabObject = null;
        }
    }

    public interface IGrabSignalReceiver
    {
        void UpdateSignal(IGrabObject grabObject);
    }
}