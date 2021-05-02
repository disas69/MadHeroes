using Cinemachine;
using DG.Tweening;
using UnityEngine;
using Framework.Tools.Misc;

namespace MadHeroes.Camera
{
    public enum CameraShakeType
    {
        None = 0,
        LightShake = 1,
        MediumShake = 2,
        HardShake = 3
    }

    public class CameraController : ActivatableMonoBehaviour
    {
        private bool _isShaking;
        private CinemachineVirtualCamera _virtualCamera;

        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private CinemachineBrain _cinemachine;
        [SerializeField] private CinemachineVirtualCamera _initialCamera;

        public void Initialize()
        {
            SwitchCamera(_initialCamera);
        }

        public void SwitchCamera(CinemachineVirtualCamera virtualCamera)
        {
            if (_virtualCamera != null)
            {
                _virtualCamera.Priority = 1;
                ResetState();
            }

            _virtualCamera = virtualCamera;
            _virtualCamera.Priority = 0;
        }

        public void SetFOV(int fov)
        {
        }

        public void Shake(int shakeType)
        {
            Shake((CameraShakeType)shakeType);
        }

        public void ResetState()
        {
            _isShaking = false;
            _virtualCamera.transform.DOKill();
        }

        private void Shake(CameraShakeType cameraShake, bool scaledTime = true)
        {
            switch (cameraShake)
            {
                case CameraShakeType.LightShake:
                    Shake(0.25f, 0.2f, 20, 90, true, !scaledTime);
                    break;
                case CameraShakeType.MediumShake:
                    Shake(0.5f, 0.4f, 25, 90, true, !scaledTime);
                    break;
                case CameraShakeType.HardShake:
                    Shake(1f, 0.7f, 30, 90, true, !scaledTime);
                    break;
            }
        }

        private void Shake(float duration, float strength, int vibrato, int randomness, bool fadeOut, bool unscaledTime)
        {
            if (_virtualCamera != null)
            {
                if (_isShaking)
                {
                    _virtualCamera.transform.DOKill();
                }

                _isShaking = true;
                _virtualCamera.transform.DOShakePosition(duration, strength, vibrato, randomness, false, fadeOut)
                    .SetUpdate(UpdateType.Fixed)
                    .SetUpdate(unscaledTime)
                    .OnComplete(() =>
                    {
                        _isShaking = false;
                    });
            }
        }
    }
}