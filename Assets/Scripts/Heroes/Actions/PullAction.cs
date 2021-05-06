using DG.Tweening;
using UnityEngine;

namespace MadHeroes.Heroes.Actions
{
    public class PullAction : SpecialAttackAction
    {
        private readonly LineRenderer _rope;

        private Hero _target;

        public PullAction(Hero hero, LineRenderer rope) : base(hero)
        {
            _rope = rope;
            _rope.gameObject.SetActive(false);
        }

        protected override void SpecialAttack(Hero hero)
        {
            _target = hero;

            if (_target != null)
            {
                DOTween.Sequence()
                    .AppendCallback(() => Hero.Animator.PlaySpecial())
                    .AppendInterval(0.5f)
                    .Append(_target.transform.DOMove(Hero.transform.position + Hero.transform.forward * 2f, 1f))
                    .AppendInterval(0.5f)
                    .OnComplete(Complete)
                    .Play();
            }
            else
            {
                DOTween.Sequence()
                    .AppendCallback(() => Hero.Animator.PlaySpecial())
                    .AppendInterval(1f)
                    .OnComplete(Complete)
                    .Play();
            }
        }

        public override void Update()
        {
            base.Update();

            if (_target != null)
            {
                _rope.gameObject.SetActive(true);
                _rope.SetPosition(0, Hero.transform.position + Vector3.up);
                _rope.SetPosition(1, _target.transform.position + Vector3.up);
            }
            else
            {
                _rope.gameObject.SetActive(false);
            }
        }

        protected override void Complete()
        {
            base.Complete();

            _rope.gameObject.SetActive(false);
            _target = null;
        }

        public override string ToString()
        {
            return "Pull";
        }
    }
}