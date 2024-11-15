namespace Code.Behaviours.Abstraction
{
    public interface IJumpBehaviour
    {
        float VerticalVelocity { get; }
        void UpdateJump(bool jumping, bool isGrounded);
    }
}
