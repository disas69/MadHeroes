using MadHeroes.Heroes.Actions;
using UnityEngine;

namespace MadHeroes.Heroes
{
    public class Otto : Hero
    {
        [SerializeField] private LineRenderer _rope;

        protected override void SetupActions()
        {
            base.SetupActions();
            Actions.Add(new MeleeAttackAction(this));
            Actions.Add(new PullAction(this, _rope));
        }
    }
}