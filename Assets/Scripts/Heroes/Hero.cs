using System.Collections.Generic;
using MadHeroes.Heroes.Actions;
using UnityEngine;

namespace MadHeroes.Heroes
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(HeroAnimator))]
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Hero : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private HeroAnimator _heroAnimator;

        [SerializeField] private float _health = 100f;

        public float Health => _health;
        public List<Action> Actions { get; private set; }
        public Action CurrentAction { get; private set; }

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

        public void AssignAction(Action action)
        {
            CurrentAction = action;
        }

        public void Execute()
        {
            CurrentAction.Start();
        }

        public void Destroy()
        {

        }

        private void Update()
        {
            if (CurrentAction != null && CurrentAction.IsActive)
            {
                CurrentAction.Update();
            }
        }
    }
}