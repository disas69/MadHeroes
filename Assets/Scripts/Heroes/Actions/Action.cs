namespace MadHeroes.Heroes.Actions
{
    public abstract class Action
    {
        protected readonly Hero Hero;

        public bool IsActive { get; private set; }

        protected Action(Hero hero)
        {
            Hero = hero;
        }

        public virtual void Start()
        {
            IsActive = true;
        }

        public virtual void Update()
        {
        }

        protected virtual void Complete()
        {
            IsActive = false;
        }

        public override string ToString()
        {
            return "Action";
        }
    }
}