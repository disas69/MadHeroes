using System;
using UnityEngine;
using UnityEngine.UI;
using MadHeroes.Players;
using System.Collections.Generic;

namespace MadHeroes.UI.Play
{
    public class PlayerView : MonoBehaviour, IDisposable
    {
        private Player _player;
        private List<HeroView> _heroViews;

        [SerializeField] private HeroView _heroView;
        [SerializeField] private Button _readyButton;
        [SerializeField] private RectTransform _content;

        public Player Player => _player;

        public void Initialize(Player player)
        {
            _player = player;
            _heroViews = new List<HeroView>(player.Heroes.Count);

            for (var i = 0; i < player.Heroes.Count; i++)
            {
                var heroView = Instantiate(_heroView, _content);
                heroView.Initialize(player.Heroes[i]);

                _heroViews.Add(heroView);
            }

            _readyButton.onClick.AddListener(player.SetReady);
        }

        public void ActivateActionsSelection(bool isActive)
        {
            for (var i = 0; i < _heroViews.Count; i++)
            {
                _heroViews[i].ActivateActionSelection(isActive);
            }

            _readyButton.gameObject.SetActive(isActive);

            if (isActive)
            {
                UpdateReadyButtonState();
            }
        }

        private void Update()
        {
            UpdateReadyButtonState();
        }

        private void UpdateReadyButtonState()
        {
            for (var i = 0; i < _heroViews.Count; i++)
            {
                var heroView = _heroViews[i];
                if (heroView.gameObject.activeSelf && !heroView.IsActionAssigned)
                {
                    _readyButton.interactable = false;
                    return;
                }
            }

            _readyButton.interactable = true;
        }

        public void Dispose()
        {
            for (var i = 0; i < _heroViews.Count; i++)
            {
                _heroViews[i].Dispose();
                Destroy(_heroViews[i].gameObject);
            }

            _readyButton.onClick.RemoveAllListeners();
        }
    }
}