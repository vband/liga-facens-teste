using System.Collections.Generic;
using Code.LevelScenes.Abstraction;

namespace Code.Services.Abstraction
{
    public interface ILevelScenesService : IService
    {
        IReadOnlyList<ILevelScene> LevelScenes { get; }
        bool IsLastLevel { get; }
        void LoadLevel(int levelIndex);
        void LoadNextLevel();
        void RestartCurrentLevel();
        void UnlockNextLevel();
    }
}