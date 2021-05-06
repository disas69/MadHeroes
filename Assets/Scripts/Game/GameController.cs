using Framework.Tools.Gameplay;
using Framework.Tools.Singleton;
using Framework.UI;
using MadHeroes.Data;
using Source.UI.Pages;
using UnityEngine;

namespace MadHeroes.Game
{
    [RequireComponent(typeof(GameSession))]
    public class GameController : MonoSingleton<GameController>
    {
        private StateMachine<GameState> _stateMachine;

        public GameState State => _stateMachine.CurrentState;
        public GameSession GameSession { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            GameSession = GetComponent<GameSession>();
        }

        private void Start()
        {
            CreateStateMachine();
            ActivateStartState();
        }

        public void SetState(GameState gameState)
        {
            _stateMachine.SetState(gameState);
        }

        private void CreateStateMachine()
        {
            _stateMachine = new StateMachine<GameState>();
            _stateMachine.AddTransition(GameState.Start, GameState.Play, ActivatePlayState);
            _stateMachine.AddTransition(GameState.Play, GameState.GameOver, ActivateGameOverState);
            _stateMachine.AddTransition(GameState.GameOver, GameState.Start, () =>
            {
                GameSession.Dispose();
                ActivateStartState();
            });
        }

        private void ActivateStartState()
        {
            NavigationManager.Instance.OpenScreen<StartPage>();
            GameSession.Initialize(GameData.LevelIndex);
        }

        private void ActivatePlayState()
        {
            NavigationManager.Instance.OpenScreen<PlayPage>();
            GameSession.Play();
        }

        private void ActivateGameOverState()
        {
            NavigationManager.Instance.OpenScreen<GameOverPage>();
            GameSession.Stop();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                GameData.Save();
            }
        }
    }
}