namespace Code.Movement.Abstraction
{
    public interface IJumper
    {
        float VerticalVelocity { get; }
        void UpdateJump(bool jumping);
    }
}
