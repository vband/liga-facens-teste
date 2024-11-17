namespace Code.Services.Abstraction
{
    public interface ILevelService : IService
    {
        bool IsLastLevel { get; }
        void LoadNextLevel();
        void RestartCurrentLevel();
    }
}