namespace Code.Models.Abstraction
{
    public interface ILevelModel
    {
        string Label { get; }
        bool Unlocked { get; set; }
        int LevelIndex { get; }
        float BestCompletionTime { get; set; }
    }
}