using DG.Tweening;

namespace MadHeroes.Heroes.Actions
{
    public class HealAction : SpecialAttackAction
    {
        private readonly float _healAmount;

        public HealAction(Hero hero, float healAmount) : base(hero)
        {
            _healAmount = healAmount;
        }

        protected override Hero FindClosestHero()
        {
            return Hero.FindClosestAlly(Hero.Configuration.SpecialRadius);
        }

        protected override void SpecialAttack(Hero hero)
        {
            DOTween.Sequence()
                .AppendCallback(() => Hero.Animator.PlaySpecial())
                .AppendInterval(0.5f)
                .AppendCallback(() => Hero.Heal(hero, _healAmount))
                .AppendInterval(1f)
                .OnComplete(Complete)
                .Play();
        }

        public override string ToString()
        {
            return "Heal";
        }
    }
}