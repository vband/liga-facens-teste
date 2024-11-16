namespace Code.Actors.Abstraction
{
    public interface IRunnerActor : IActor
    {
        float HorizontalPos { get; }
        float HorizontalSpeed { get; }
        void UpdateMovement(float axis);
    }
}