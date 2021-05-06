using DG.Tweening;
using UnityEngine;

namespace MadHeroes.Heroes.Actions
{
    public abstract class AttackAction : Action
    {
        protected AttackAction(Hero hero) : base(hero)
        {
        }

        public override void Start()
        {
            base.Start();

            var enemy = Hero.FindClosestEnemy(Hero.Configuration.AttackRadius);
            if (enemy != null)
            {
                var direction = (enemy.transform.position - Hero.transform.position).normalized;

                DOTween.Sequence()
                    .Append(Hero.transform.DORotateQuaternion(Quaternion.LookRotation(direction), 1f))
                    .AppendCallback(() => Attack(enemy))
                    .Play();
            }
            else
            {
                Attack(null);
            }
        }

        protected abstract void Attack(Hero enemy);

        public override string ToString()
        {
            return "Attack";
        }
    }
}