using MadHeroes.Heroes.Actions;
using MadHeroes.Heroes.Weapon;
using UnityEngine;

namespace MadHeroes.Heroes
{
    public class Alice : Hero
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private float _healAmount;

        protected override void SetupActions()
        {
            base.SetupActions();
            Actions.Add(new RangedAttackAction(this, _projectile));
            Actions.Add(new HealAction(this, _healAmount));
        }
    }
}