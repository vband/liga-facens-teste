namespace Code.Behaviours.Abstraction
{
    public interface IBounceableBehaviour : IBehaviour
    {
        void UpdateBounce(float verticalVelocity);
    }
}