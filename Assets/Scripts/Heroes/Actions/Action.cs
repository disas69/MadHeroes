namespace MadHeroes.Heroes.Actions
{
    public abstract class Action
    {
        public abstract void Start();
        public abstract void Update();
        public abstract void OnComplete();
    }
}
