using Code.Behaviours.Abstraction;

namespace Code.Behaviours.Concrete
{
    public class KillBehaviour : IKillBehaviour
    {
        public void Kill(IKillableBehaviour killableBehaviour)
        {
            killableBehaviour.Die();
        }
    }
}