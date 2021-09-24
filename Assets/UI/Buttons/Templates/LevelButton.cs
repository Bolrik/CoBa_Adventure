using Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class LevelButton : GameButton
    {
        [SerializeField] private Text worldText;
        Text WorldText { get { return worldText; } }

        public int LevelIndex { get; set; }
        public int WorldIndex { get; set; }

        float AnimationRotation { get; set; }
        float Speed { get; set; } = 20f;
        float Range { get; set; } = 5f;

        private void Start()
        {
            this.AnimationRotation += UnityEngine.Random.value * this.Range * 2;

            if (this.WorldIndex > 0)
                this.WorldText.text = $"{this.WorldIndex}";
        }

        public override void OnClick()
        {
            LevelManager.Instance.Load(this.WorldIndex, this.LevelIndex);
        }

        private void Update()
        {
            this.AnimationRotation += Time.deltaTime * this.Speed;

            float fullCircleRange = this.Range * 4;

            if (this.AnimationRotation >= fullCircleRange)
                this.AnimationRotation -= fullCircleRange;

            float rotationZ = this.AnimationRotation.PingPong(-this.Range, this.Range);


            //Quaternion rotation = this.transform.rotation;
            //rotation.eulerAngles = new Vector3(0, 0, rotationZ);
            //this.transform.rotation = rotation;
            this.transform.localEulerAngles = new Vector3(0, 0, rotationZ);
        }
    }
}