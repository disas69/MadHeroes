using UnityEngine;

namespace MadHeroes.Heroes
{
    [RequireComponent(typeof(Animator))]
    public class HeroAnimator : MonoBehaviour
    {
        private const float TransitionDuration = 0.2f;

        private readonly int _meleeAtack = Animator.StringToHash("Melee");
        private readonly int _rangedAtack = Animator.StringToHash("Ranged");
        private readonly int _damage = Animator.StringToHash("Damage");
        private readonly int _death = Animator.StringToHash("Death");
        private readonly int _special = Animator.StringToHash("Special");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayMeleeAttack()
        {
            _animator.CrossFade(_meleeAtack, TransitionDuration);
        }

        public void PlayRangedAttack()
        {
            _animator.CrossFade(_rangedAtack, TransitionDuration);
        }

        public void PlayDamage()
        {
            _animator.CrossFade(_damage, TransitionDuration);
        }

        public void PlayDeath()
        {
            _animator.CrossFade(_death, TransitionDuration);
        }

        public void PlaySpecial()
        {
            _animator.CrossFade(_special, TransitionDuration);
        }
    }
}