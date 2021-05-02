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

        protected List<Action> Actions;

        [SerializeField] private float _health = 100f;

        public float Health => _health;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _heroAnimator = GetComponent<HeroAnimator>();

            InitializeActions();
        }

        protected virtual void InitializeActions()
        {
            Actions = new List<Action>
            {
                new MoveAction(this)
            };
        }

        public List<Action> GetActions()
        {
            return Actions;
        }

        public void Destroy()
        {

        }
    }
}