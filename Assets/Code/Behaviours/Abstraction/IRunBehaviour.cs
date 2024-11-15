namespace Code.Behaviours.Abstraction
{
    public interface IRunBehaviour
    {
        float HorizontalVelocity { get; }
        void UpdateMovement(float axis);
    }
}
