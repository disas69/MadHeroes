using System;
using UnityEngine;
using MadHeroes.Heroes.Actions;
using System.Collections.Generic;
using MadHeroes.Configuration;
using MadHeroes.Game;
using MadHeroes.Players;
using Action = MadHeroes.Heroes.Actions.Action;

namespace MadHeroes.Heroes
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(HeroAnimator))]
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(HeroInputHandler))]
    public abstract class Hero : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private HeroAnimator _heroAnimator;
        private HeroInputHandler _inputHandler;

        public bool IsAlive { get; private set; }
        public Player Player { get; private set; }
        public float Health { get; private set; }
        public HeroConfiguration Configuration { get; private set; }
        public List<Action> Actions { get; private set; }
        public Action CurrentAction { get; private set; }
        public float Velocity { get; set; }
        public bool IsComplete => !IsMoving() && CurrentAction != null && !CurrentAction.IsActive;
        public HeroAnimator Animator => _heroAnimator;

        public event Action<float> HealthChanged;
        public event System.Action Died;

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

        public void Initialize(Player player, HeroConfiguration configuration)
        {
            IsAlive = true;
            Player = player;
            Configuration = configuration;
            Health = configuration.Health;
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

        public void Attack(Hero enemy)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(Configuration.Damage);
                enemy.Animator.PlayDamage();
            }
        }

        public bool IsMoving()
        {
            var velocity = _rigidbody.velocity.magnitude;
            return velocity > 0.1f;
        }

        public Hero FindClosestEnemy()
        {
            return FindClosestHero(Configuration.EnemyRadius, hero => hero.IsAlive && hero.Player != Player);
        }

        public Hero FindClosestAlly()
        {
            return FindClosestHero(Configuration.AllyRadius, hero => hero.IsAlive && hero.Player == Player);
        }

        public Hero FindClosestHero(float radius, Func<Hero, bool> searchFunc)
        {
            Hero closestHero = null;
            var minDistance = float.MaxValue;

            var heroes = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Character"));
            for (var i = 0; i < heroes.Length; i++)
            {
                var hero = heroes[i].GetComponent<Hero>();
                if (hero != null && hero != this && searchFunc.Invoke(hero))
                {
                    var distance = Vector3.Distance(transform.position, hero.transform.position);
                    if (distance < minDistance)
                    {
                        closestHero = hero;
                        minDistance = distance;
                    }
                }
            }

            return closestHero;
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

        private void TakeDamage(float damage)
        {
            Health = Mathf.Clamp(Health - damage, 0f, Configuration.Health);
            HealthChanged?.Invoke(Health);
        }

        public void Die()
        {
            IsAlive = false;
            Animator.PlayDeath();
            Died?.Invoke();
        }
    }
}