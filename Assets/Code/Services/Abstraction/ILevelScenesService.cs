using System.Collections.Generic;
using Code.Models.Abstraction;

namespace Code.Services.Abstraction
{
    public interface ILevelScenesService : IService
    {
        IReadOnlyList<ILevelSceneModel> LevelScenes { get; }
        bool IsLastLevel { get; }
        int CurrentLevelIndex { get; }
        void LoadLevel(int levelIndex);
        void LoadNextLevel();
        void RestartCurrentLevel();
    }
}