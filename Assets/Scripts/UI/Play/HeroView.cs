using System;
using UnityEngine;
using UnityEngine.UI;
using MadHeroes.Heroes;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Action = MadHeroes.Heroes.Actions.Action;

namespace MadHeroes.UI.Play
{
    public class HeroView : MonoBehaviour, IDisposable
    {
        private Hero _hero;
        private AsyncOperationHandle<Sprite> _iconHandle;
        private List<ActionToggleView> _actionToggles;

        [SerializeField] private Image _portrait;
        [SerializeField] private Image _health;
        [SerializeField] private ToggleGroup _actions;
        [SerializeField] private ActionToggleView _actionToggleView;

        public bool IsActionAssigned => _actions.AnyTogglesOn();

        public void Initialize(Hero hero)
        {
            _hero = hero;
            _hero.HealthChanged += UpdateHealth;
            _hero.Died += OnHeroDied;

            UpdateHealth(hero.Health);

            _portrait.enabled = false;
            _iconHandle = Addressables.LoadAssetAsync<Sprite>(hero.Configuration.Icon.RuntimeKey);
            _iconHandle.Completed += OnIconLoaded;

            _actionToggles = new List<ActionToggleView>(hero.Actions.Count);

            for (var i = 0; i < hero.Actions.Count; i++)
            {
                var actionToggle = Instantiate(_actionToggleView, _actions.transform);
                actionToggle.Initialize(hero.Actions[i]);
                actionToggle.ToggleOn += OnActionToggleOn;

                _actions.RegisterToggle(actionToggle.Toggle);
                _actionToggles.Add(actionToggle);
            }

            _actions.SetAllTogglesOff(false);
        }

        public void ActivateActionSelection(bool isActive)
        {
            _actions.gameObject.SetActive(isActive);

            if (isActive)
            {
                _actions.SetAllTogglesOff(false);
            }
        }

        private void OnActionToggleOn(Toggle toggle, Action action)
        {
            _actions.NotifyToggleOn(toggle, false);
            _hero.AssignAction(action);
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

        private void OnHeroDied()
        {
            gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _hero.HealthChanged -= UpdateHealth;
            _iconHandle.Completed -= OnIconLoaded;
            _hero.Died -= OnHeroDied;

            for (var i = 0; i < _actionToggles.Count; i++)
            {
                var actionToggle = _actionToggles[i];
                actionToggle.ToggleOn -= OnActionToggleOn;

                _actions.UnregisterToggle(actionToggle.Toggle);
                Destroy(actionToggle.gameObject);
            }

            Addressables.Release(_iconHandle);
        }
    }
}
