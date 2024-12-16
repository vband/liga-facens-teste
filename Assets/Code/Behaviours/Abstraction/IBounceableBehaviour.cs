namespace Code.Behaviours.Abstraction
{
    public interface IBounceableBehaviour : IBehaviour
    {
        bool Enabled { get; set; }
        void UpdateBounce(float verticalVelocity);
    }
}