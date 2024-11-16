namespace Code.Behaviours.Abstraction
{
    public interface IRunBehaviour : IBehaviour
    {
        float HorizontalVelocity { get; }
        void UpdateMovement(float axis);
    }
}
