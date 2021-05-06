using DG.Tweening;

namespace MadHeroes.Heroes.Actions
{
    public class PushAction : SpecialAttackAction
    {
        private readonly float _pushForce;

        public PushAction(Hero hero, float pushForce) : base(hero)
        {
            _pushForce = pushForce;
        }

        protected override void SpecialAttack(Hero hero)
        {
            DOTween.Sequence()
                .AppendCallback(() => Hero.Animator.PlaySpecial())
                .AppendInterval(0.5f)
                .AppendCallback(() =>
                {
                    if (hero != null)
                    {
                        var direction = (hero.transform.position - Hero.transform.position).normalized;
                        hero.AddForce(direction * _pushForce);
                    }
                })
                .AppendInterval(0.5f)
                .OnComplete(Complete)
                .Play();
        }

        public override string ToString()
        {
            return "Push";
        }
    }
}