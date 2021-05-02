using System;
using UnityEngine;
using UnityEngine.UI;
using MadHeroes.Heroes;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MadHeroes.UI.Play
{
    public class HeroView : MonoBehaviour, IDisposable
    {
        private Hero _hero;
        private AsyncOperationHandle<Sprite> _iconHandle;

        [SerializeField] private Image _portrait;
        [SerializeField] private Image _health;

        public void Initialize(Hero hero)
        {
            _hero = hero;
            _hero.HealthChanged += UpdateHealth;

            _portrait.enabled = false;
            _iconHandle = Addressables.LoadAssetAsync<Sprite>(hero.Configuration.Icon.RuntimeKey);
            _iconHandle.Completed += OnIconLoaded;

            UpdateHealth(hero.Health);
        }

        private void OnIconLoaded(AsyncOperationHandle<Sprite> handle)
        {
            _portrait.enabled = true;
            _portrait.sprite = handle.Result;
        }

        private void UpdateHealth(float health)
        {
            var max = _hero.Configuration.Health;
            var ratio = _hero.Health / max;

            _health.fillAmount = ratio;
        }

        public void Dispose()
        {
            _hero.HealthChanged -= UpdateHealth;
            _iconHandle.Completed -= OnIconLoaded;
            Addressables.Release(_iconHandle);
        }
    }
}
