using DG.Tweening;

namespace MadHeroes.Heroes.Actions
{
    public class MeleeAttackAction : AttackAction
    {
        public MeleeAttackAction(Hero hero) : base(hero)
        {
        }

        protected override void Attack(Hero enemy)
        {
            DOTween.Sequence()
                .AppendCallback(() => Hero.Animator.PlayMeleeAttack())
                .AppendInterval(0.5f)
                .AppendCallback(() => Hero.Attack(enemy))
                .AppendInterval(0.5f)
                .OnComplete(Complete)
                .Play();
        }
    }
}