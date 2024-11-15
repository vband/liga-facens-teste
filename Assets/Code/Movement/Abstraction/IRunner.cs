namespace Code.Movement.Abstraction
{
    public interface IRunner
    {
        float HorizontalVelocity { get; }
        void UpdateMovement(float axis);
    }
}
