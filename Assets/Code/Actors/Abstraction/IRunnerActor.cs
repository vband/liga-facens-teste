namespace Code.Actors.Abstraction
{
    public interface IRunnerActor : IActor
    {
        void UpdateMovement(float axis);
    }
}