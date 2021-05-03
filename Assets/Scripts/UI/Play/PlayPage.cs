using UnityEngine;
using MadHeroes.Game;
using MadHeroes.UI.Play;
using System.Collections.Generic;
using MadHeroes.Game.Loop.Phases;
using Framework.UI.Structure.Base.Model;
using Framework.UI.Structure.Base.View;

namespace Source.UI.Pages
{
    public class PlayPage : Page<PageModel>
    {
        private List<PlayerView> _playerViews;

        [SerializeField] private PlayerView _playerViewLeft;
        [SerializeField] private PlayerView _playerViewRight;
        [SerializeField] private RectTransform _playersRoot;

        public override void OnEnter()
        {
            base.OnEnter();

            _playerViews = new List<PlayerView>();

            var players = GameController.Instance.GameSession.Level.Players;
            for (var i = 0; i < players.Length; i++)
            {
                var playerView = Instantiate(i % 2 == 0 ? _playerViewLeft : _playerViewRight, _playersRoot);
                playerView.Initialize(players[i]);

                _playerViews.Add(playerView);
            }

            GameController.Instance.GameSession.GameLoop.PhaseActivated += OnPhaseActivated;
        }

        private void OnPhaseActivated(Phase phase)
        {
            if (phase is AssignActionPhase assignActionPhase)
            {
                var activePlayer = assignActionPhase.GetActivePlayer();

                for (var i = 0; i < _playerViews.Count; i++)
                {
                    var playerView = _playerViews[i];
                    if (playerView.Player == activePlayer)
                    {
                        playerView.gameObject.SetActive(true);
                        playerView.ActivateActionsSelection(true);
                    }
                    else
                    {
                        playerView.gameObject.SetActive(false);
                    }
                }
            }
            else if (phase is MovePhase)
            {
                for (var i = 0; i < _playerViews.Count; i++)
                {
                    _playerViews[i].gameObject.SetActive(true);
                    _playerViews[i].ActivateActionsSelection(false); ;
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            GameController.Instance.GameSession.GameLoop.PhaseActivated -= OnPhaseActivated;

            for (var i = 0; i < _playerViews.Count; i++)
            {
                _playerViews[i].Dispose();
                Destroy(_playerViews[i].gameObject);
            }
        }
    }
}