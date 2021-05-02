using Framework.Utils.Math;
using UnityEngine;

namespace MadHeroes.Camera
{
    [RequireComponent(typeof(CameraController))]
    public class CameraScaler : MonoBehaviour
    {
        private CameraController _camera;
        private int _width = -1;
        private int _height = -1;

        public float FOV = 60f;
        public MinMaxValue SizeBounds;

        private void Awake()
        {
            _camera = GetComponent<CameraController>();
        }

        private void Update()
        {
            if (IsScreenSizeChanged())
            {
                var halfWidth = Mathf.Tan(0.5f * FOV * Mathf.Deg2Rad);
                var halfHeight = halfWidth * Screen.height / Screen.width;
                var verticalFoV = Mathf.CeilToInt(2.0f * Mathf.Atan(halfHeight) * Mathf.Rad2Deg);

                _camera.SetFOV((int) Mathf.Clamp(verticalFoV, SizeBounds.Min, SizeBounds.Max));
            }
        }

        private bool IsScreenSizeChanged()
        {
            var value = Screen.width != _width || Screen.height != _height;
            if (value)
            {
                _width = Screen.width;
                _height = Screen.height;
            }

            return value;
        }
    }
}