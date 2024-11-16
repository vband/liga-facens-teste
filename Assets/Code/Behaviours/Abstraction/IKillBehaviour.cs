namespace Code.Behaviours.Abstraction
{
    public interface IKillBehaviour : IBehaviour
    {
        void Kill(IKillableBehaviour killableBehaviour);
    }
}