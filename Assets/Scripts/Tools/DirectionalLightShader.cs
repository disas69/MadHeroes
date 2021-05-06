using UnityEngine;

namespace MadHeroes.Tools
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Light))]
    public class DirectionalLightShader : MonoBehaviour
    {
        private Light _light;

        private void Awake()
        {
            _light = GetComponent<Light>();
        }

        private void Update()
        {
            if (_light != null)
            {
                Shader.SetGlobalVector("_LightDirectionVec", -transform.forward);
                Shader.SetGlobalFloat("_LightIntensity", _light.intensity);
            }
        }
    }
}