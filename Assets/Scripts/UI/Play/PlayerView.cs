using System;
using UnityEngine;
using MadHeroes.Players;
using System.Collections.Generic;

namespace MadHeroes.UI.Play
{
    public class PlayerView : MonoBehaviour, IDisposable
    {
        private Player _player;
        private List<HeroView> _heroViews;

        [SerializeField] private HeroView _heroView;
        public Player Player => _player;

        public void Initialize(Player player)
        {
            _player = player;
            _heroViews = new List<HeroView>(player.Heroes.Count);

            for (var i = 0; i < player.Heroes.Count; i++)
            {
                var heroView = Instantiate(_heroView, transform);
                heroView.Initialize(player.Heroes[i]);

                _heroViews.Add(heroView);
            }
        }

        public void Dispose()
        {
            for (var i = 0; i < _heroViews.Count; i++)
            {
                _heroViews[i].Dispose();
                Destroy(_heroViews[i].gameObject);
            }
        }
    }
}