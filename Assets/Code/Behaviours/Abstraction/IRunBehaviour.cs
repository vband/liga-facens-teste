namespace Code.Behaviours.Abstraction
{
    public interface IRunBehaviour : IBehaviour
    {
        float HorizontalVelocity { get; }
        float HorizontalPos { get; }
        void UpdateMovement(float axis);
        void SnapHorizontalPos(float horizontalPosition);
    }
}
