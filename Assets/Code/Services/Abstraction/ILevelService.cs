namespace Code.Services.Abstraction
{
    public interface ILevelService : IService
    {
        bool IsLastLevel { get; }
        void LoadFirstLevel();
        void LoadNextLevel();
        void RestartCurrentLevel();
    }
}