using DG.Tweening;
using UnityEngine;

namespace MadHeroes.Heroes.Actions
{
    public abstract class SpecialAttackAction : Action
    {
        protected SpecialAttackAction(Hero hero) : base(hero)
        {
        }

        public override void Start()
        {
            base.Start();

            var hero = FindClosestHero();
            if (hero != null)
            {
                var direction = (hero.transform.position - Hero.transform.position).normalized;

                DOTween.Sequence()
                    .Append(Hero.transform.DORotateQuaternion(Quaternion.LookRotation(direction), 1f))
                    .AppendCallback(() => SpecialAttack(hero))
                    .Play();
            }
            else
            {
                SpecialAttack(null);
            }
        }

        protected virtual Hero FindClosestHero()
        {
            return Hero.FindClosestEnemy(Hero.Configuration.SpecialRadius);
        }

        protected abstract void SpecialAttack(Hero hero);

        public override string ToString()
        {
            return "Special";
        }
    }
}
