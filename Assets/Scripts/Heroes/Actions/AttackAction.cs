namespace MadHeroes.Heroes.Actions
{
    public class AttackAction : Action
    {
        public AttackAction(Hero hero) : base(hero)
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
            return "Attack";
        }
    }
}