using DG.Tweening;

namespace MadHeroes.Heroes.Actions
{
    public class MeleeAttackAction : AttackAction
    {
        public MeleeAttackAction(Hero hero) : base(hero)
        {
        }

        public override void Attack(Hero enemy)
        {
            base.Attack(enemy);

            DOTween.Sequence()
                .AppendCallback(() => Hero.Animator.PlayMeleeAttack())
                .AppendInterval(0.5f)
                .AppendCallback(() => Hero.Attack(enemy))
                .AppendInterval(1f)
                .OnComplete(Complete)
                .Play();
        }
    }
}