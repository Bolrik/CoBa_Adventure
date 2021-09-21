using Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SlingMechanic
{
    public abstract class GrabObject : MonoBehaviour, IGrabObject
    {
        [SerializeField] private Rigidbody2D body;
        public Rigidbody2D Body { get { return body; } protected set { body = value; } }

        [SerializeField] private GrabType grabType;
        public GrabType GrabType { get { return grabType; } private set { grabType = value; } }

        public bool IsGrabbable { get; private set; }

        private void Awake()
        {
            this.Body = this.GetComponent<Rigidbody2D>();
            this.OnAwake();
        }

        private void Start()
        {
            this.OnStart();

            this.SetIsGrabbable(true);
        }

        protected virtual void OnAwake() { }
        protected virtual void OnStart() { }



        #region Grab Events
        public virtual void OnTouch() { }

        public virtual void OnGrab() { }

        public virtual void OnSling() { }

        public virtual void OnDrag() { }
        #endregion

        protected void SetIsGrabbable(bool value)
        {
            if (this.IsGrabbable == value)
                return;

            this.IsGrabbable = value;

            if (this.IsGrabbable)
            {
                GrabManager.Instance.Register(this);
            }
            else
            {
                GrabManager.Instance.Unregister(this);
            }
        }

        public abstract bool IsGrabbableFromPosition(Vector3 worldPosition);
    }

    public interface IGrabObject
    {
        Transform transform { get; }
        bool IsGrabbable { get; }
        GrabType GrabType { get; }
        Rigidbody2D Body { get; }

        bool IsGrabbableFromPosition(Vector3 worldPosition);

        void OnTouch();
        void OnGrab();
        void OnSling();
        void OnDrag();
    }

    public interface IColorGrabObject
    {
        ColorCode GetColorCode();
    }
}