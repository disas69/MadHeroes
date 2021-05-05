using UnityEngine;

namespace MadHeroes.Tools
{
    [RequireComponent(typeof(LineRenderer))]
    public class CircleRenderer : MonoBehaviour
    {
        private LineRenderer _lineRenderer;

        [Range(0.1f, 100f)] private float _radius = 1.0f;
        [Range(3, 256)] private int _numSegments = 64;
        [SerializeField] private Color _color;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            Render();
        }

        public void Set(float radius, Color color)
        {
            _radius = radius;
            _color = color;

            Render();
        }

        public void Render()
        {
            _lineRenderer.SetColors(_color, _color);
            _lineRenderer.SetWidth(0.3f, 0.3f);
            _lineRenderer.SetVertexCount(_numSegments + 1);
            _lineRenderer.useWorldSpace = false;

            float deltaTheta = (float) (2.0 * Mathf.PI) / _numSegments;
            float theta = 0f;

            for (int i = 0; i < _numSegments + 1; i++)
            {
                float x = _radius * Mathf.Cos(theta);
                float z = _radius * Mathf.Sin(theta);
                Vector3 position = new Vector3(x, 0, z);

                _lineRenderer.SetPosition(i, position);
                theta += deltaTheta;
            }
        }
    }
}