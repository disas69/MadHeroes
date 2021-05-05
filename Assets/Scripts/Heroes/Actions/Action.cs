namespace MadHeroes.Heroes.Actions
{
    public abstract class Action
    {
        protected Hero Hero;

        public bool IsActive { get; private set; }

        protected Action(Hero hero)
        {
            Hero = hero;
        }

        public virtual void Start()
        {
            IsActive = true;
        }

        public abstract void Update();

        public virtual void Complete()
        {
            IsActive = false;
        }

        public override string ToString()
        {
            return "Action";
        }
    }
}