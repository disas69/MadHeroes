using System;
using UnityEngine;
using MadHeroes.Heroes.Actions;
using System.Collections.Generic;
using MadHeroes.Configuration;
using MadHeroes.Game;
using Action = MadHeroes.Heroes.Actions.Action;

namespace MadHeroes.Heroes
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(HeroAnimator))]
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(HeroInputHandler))]
    public abstract class Hero : MonoBehaviour
    {
        private float _health;
        private Rigidbody _rigidbody;
        private HeroAnimator _heroAnimator;
        private HeroConfiguration _configuration;
        private HeroInputHandler _inputHandler;

        public float Health => _health;
        public HeroConfiguration Configuration => _configuration;
        public List<Action> Actions { get; private set; }
        public Action CurrentAction { get; private set; }
        public float Velocity { get; set; }
        public bool IsComplete => !IsMoving() && CurrentAction != null && !CurrentAction.IsActive;

        public event Action<float> HealthChanged;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _heroAnimator = GetComponent<HeroAnimator>();
            _inputHandler = GetComponent<HeroInputHandler>();

            SetupActions();
        }

        protected virtual void SetupActions()
        {
            Actions = new List<Action>
            {
                new MoveAction(this)
            };
        }

        public void Initialize(HeroConfiguration configuration)
        {
            _configuration = configuration;
            _health = configuration.Health;
        }

        public void AssignAction(Action action)
        {
            CurrentAction = action;

            if (CurrentAction is MoveAction)
            {
                _inputHandler.Activate(true);
                Velocity = GameConfiguration.GameSettings.DefaultMoveVelocity;
            }
            else
            {
                _inputHandler.Activate(false);
            }
        }

        public void Execute()
        {
            CurrentAction.Start();

            if (CurrentAction is MoveAction)
            {
                _inputHandler.Activate(false);
            }
        }

        public void UpdateAction()
        {
            if (CurrentAction != null && CurrentAction.IsActive)
            {
                CurrentAction.Update();
            }
        }

        public void Move()
        {
            _rigidbody.AddForce(transform.forward * Velocity, ForceMode.Impulse);
        }

        public bool IsMoving()
        {
            var velocity = _rigidbody.velocity.magnitude;
            return velocity > 0.1f;
        }

        private void Update()
        {
            if (_inputHandler.IsActive && _inputHandler.IsTouching)
            {
                Velocity = GameConfiguration.GameSettings.MoveVelocity.Clamp(_inputHandler.Velocity);

                var direction = _inputHandler.Direction;
                if (GameController.Instance.GameSession.Camera.Angle < 180f)
                {
                    direction *= -1f;
                }

                var rotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.y), Vector3.up);
                transform.rotation = rotation;
            }
        }

        public void Stop()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        public void Destroy()
        {

        }
    }
}