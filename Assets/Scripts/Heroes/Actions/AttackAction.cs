using DG.Tweening;
using UnityEngine;

namespace MadHeroes.Heroes.Actions
{
    public class AttackAction : Action
    {
        public AttackAction(Hero hero) : base(hero)
        {
        }

        public override void Start()
        {
            base.Start();

            var enemy = Hero.FindClosestEnemy();
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

        public virtual void Attack(Hero enemy)
        {
        }

        public override string ToString()
        {
            return "Attack";
        }
    }
}