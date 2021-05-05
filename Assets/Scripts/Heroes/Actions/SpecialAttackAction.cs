namespace MadHeroes.Heroes.Actions
{
    public class SpecialAttackAction : Action
    {
        public SpecialAttackAction(Hero hero) : base(hero)
        {
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {

        }

        public override string ToString()
        {
            return "Special";
        }
    }
}
