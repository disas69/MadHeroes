using UnityEngine;

namespace MadHeroes.Heroes
{
    [RequireComponent(typeof(Animator))]
    public class HeroAnimator : MonoBehaviour
    {
        private readonly int _meleeAtack = Animator.StringToHash("Melee");
        private readonly int _rangedAtack = Animator.StringToHash("Ranged");
        private readonly int _damage = Animator.StringToHash("Damage");
        private readonly int _death = Animator.StringToHash("Death");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayMeleeAttack()
        {
            _animator.CrossFade(_meleeAtack, 0.2f);
        }

        public void PlayRangedAttack()
        {
            _animator.CrossFade(_rangedAtack, 0.2f);
        }

        public void PlayDamage()
        {
            _animator.CrossFade(_damage, 0.2f);
        }

        public void PlayDeath()
        {
            _animator.CrossFade(_death, 0.2f);
        }
    }
}