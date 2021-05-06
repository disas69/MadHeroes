using DG.Tweening;

namespace MadHeroes.Heroes.Actions
{
    public class MoveAction : Action
    {
        private bool _isActive;

        public MoveAction(Hero hero) : base(hero)
        {
        }

        public override void Start()
        {
            base.Start();

            _isActive = false;

            DOTween.Sequence()
                .AppendInterval(1f)
                .OnComplete(() =>
                {
                    _isActive = true;
                    Hero.Move();
                })
                .Play();
        }

        public override void Update()
        {
            if (_isActive && !Hero.IsMoving())
            {
                Complete();
            }
        }

        protected override void Complete()
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