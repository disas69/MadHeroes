using DG.Tweening;
using UnityEngine;
using MadHeroes.Heroes.Weapon;

namespace MadHeroes.Heroes.Actions
{
    public class RangedAttackAction : AttackAction
    {
        private readonly Projectile _projectile;

        public RangedAttackAction(Hero hero, Projectile projectile) : base(hero)
        {
            _projectile = Object.Instantiate(projectile, Hero.transform);
            _projectile.gameObject.SetActive(false);
        }

        public override void Attack(Hero enemy)
        {
            base.Attack(enemy);

            DOTween.Sequence()
                .AppendCallback(() => Hero.Animator.PlayRangedAttack())
                .AppendInterval(0.25f)
                .AppendCallback(() =>
                {
                    _projectile.gameObject.SetActive(true);
                    _projectile.transform.position = Hero.transform.position + Vector3.up;
                })
                .Append(_projectile.transform.DOMove(enemy != null ? enemy.transform.position + Vector3.up : Hero.transform.position + Vector3.up + Hero.transform.forward * 5f, 0.5f))
                .AppendCallback(() =>
                {
                    _projectile.gameObject.SetActive(false);
                    Hero.Attack(enemy);
                })
                .AppendInterval(1f)
                .OnComplete(Complete)
                .Play();
        }
    }
}