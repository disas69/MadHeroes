namespace MadHeroes.Heroes.Actions
{
    public abstract class Action
    {
        protected Hero Hero;

        public bool IsActive { get; }

        protected Action(Hero hero)
        {
            Hero = hero;
        }

        public abstract void Start();
        public abstract void Update();
        public abstract void OnComplete();
    }
}
