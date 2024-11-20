namespace Code.Behaviours.Abstraction
{
    public interface IJumpBehaviour : IBehaviour
    {
        float VerticalVelocity { get; }
        void UpdateJump(bool jumping);
        void SetGrounded(bool grounded);
    }
}
