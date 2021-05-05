using UnityEngine;

namespace MadHeroes.Heroes.Actions
{
    public class MoveAction : Action
    {
        private float _time;
        private bool _executed;

        public MoveAction(Hero hero) : base(hero)
        {
        }

        public override void Start()
        {
            base.Start();

            _time = Time.time;
            _executed = false;
        }

        public override void Update()
        {
            if (_executed)
            {
                if (!Hero.IsMoving())
                {
                    Complete();
                }
            }
            else
            {
                if (Time.time - _time > 1.5f)
                {
                    _executed = true;
                    Hero.Move();
                }
            }
        }

        public override void Complete()
        {
            base.Complete();
            Hero.Stop();
        }

        public override string ToString()
        {
            return "Move";
        }
    }
}