namespace Code.Actors.Abstraction
{
    public interface IRunnerActor : IActor
    {
        float HorizontalPos { get; }
        void UpdateMovement(float axis);
        void SnapHorizontalPos(float targetHorizontalPos);
    }
}