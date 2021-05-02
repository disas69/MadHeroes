using System;
using UnityEngine;
using MadHeroes.Heroes.Actions;
using System.Collections.Generic;
using Action = MadHeroes.Heroes.Actions.Action;

namespace MadHeroes.Heroes
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(HeroAnimator))]
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Hero : MonoBehaviour
    {
        private float _health;
        private Rigidbody _rigidbody;
        private HeroAnimator _heroAnimator;
        private HeroConfiguration _configuration;

        public float Health => _health;
        public HeroConfiguration Configuration => _configuration;
        public List<Action> Actions { get; private set; }
        public Action CurrentAction { get; private set; }

        public event Action<float> HealthChanged;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _heroAnimator = GetComponent<HeroAnimator>();

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
        }

        public void Execute()
        {
            CurrentAction.Start();
        }

        private void Update()
        {
            if (CurrentAction != null && CurrentAction.IsActive)
            {
                CurrentAction.Update();
            }
        }

        public void Destroy()
        {

        }
    }
}