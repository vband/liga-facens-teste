namespace Code.Actors.Abstraction
{
    public interface IJumperActor : IActor
    {
        void UpdateJump(bool jumping);
    }
}