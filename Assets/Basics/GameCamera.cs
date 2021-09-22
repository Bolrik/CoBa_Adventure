using Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

namespace Basics
{
    public class GameCamera : MonoBehaviour
    {
        #region Singleton Pattern		
        public static GameCamera Instance { get; private set; }

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

            this.AwakeInit();
        }
        #endregion


        [SerializeField] private float zoomSpeed = 3.5f;
        public float ZoomSpeed { get { return zoomSpeed; } }

        [SerializeField] private float speed = 10f;
        public float Speed { get { return speed; } }


        [SerializeField] private PixelPerfectCamera pixelPerfectCamera;
        public PixelPerfectCamera PixelPerfectCamera { get { return pixelPerfectCamera; } }

        public int CameraHeight { get => this.Camera.scaledPixelHeight; }

        Vector3 LastMousePosition { get; set; }

        public Camera Camera { get; private set; }


        void AwakeInit()
        {
            this.Camera = this.GetComponent<Camera>();
            LevelManager.Instance.PostLoadLevel += this.ResetCamera;
        }

        public Vector2 GetWorldPosition()
        {
            return this.Camera.ScreenToWorldPoint(Input.mousePosition);
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().buildIndex != 2)
            {
                return;
            }

            if (Input.mouseScrollDelta.y != 0)
            {
                int ppu = this.PixelPerfectCamera.assetsPPU;
                this.PixelPerfectCamera.assetsPPU =
                    ppu + (int)(Mathf.Sign(Input.mouseScrollDelta.y) * Mathf.Max(Mathf.Abs(this.ZoomSpeed * ppu * Time.deltaTime), 1));
                //Mathf.Clamp(ppu + (int)(Mathf.Sign(Input.mouseScrollDelta.y) * Mathf.Max(Mathf.Abs(this.ZoomSpeed * ppu * Time.deltaTime), 1)),
                //    64, 164);
            }

            // Move
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.position += Vector3.up * Time.deltaTime * this.Speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.transform.position -= Vector3.up * Time.deltaTime * this.Speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.position += Vector3.right * Time.deltaTime * this.Speed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.position -= Vector3.right * Time.deltaTime * this.Speed;
            }

            Vector3 delta = this.LastMousePosition - Input.mousePosition;
            this.LastMousePosition = Input.mousePosition;

            if (Input.GetMouseButton(1))
            {
                // this.transform.position += (delta / this.Camera.orthographicSize * 5) * Time.smoothDeltaTime;
                this.transform.position += (delta * this.Camera.orthographicSize / 5) * Time.smoothDeltaTime;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                this.ResetCamera();
            }
        }

        private void ResetCamera()
        {
            if (LevelManager.Instance.Level == null)
            {
                this.transform.position = Vector3.zero + Vector3.forward * -10;
            }
            else
            {
                Level.Level level = LevelManager.Instance.Level;
                level.GetCameraDefaults(out float x, out float y, out int ppu);

                this.transform.position = new Vector3(x, y) + Vector3.forward * -10;
                this.PixelPerfectCamera.assetsPPU = ppu;
            }
        }

        public void Offset(int x, int y)
        {
            this.transform.position += new Vector3(x, y);
        }
    }
}