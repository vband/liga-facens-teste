namespace Code.Behaviours.Abstraction
{
    public interface IJumpBehaviour : IBehaviour
    {
        float VerticalVelocity { get; }
        void UpdateJump(bool jumping, bool isGrounded);
    }
}
