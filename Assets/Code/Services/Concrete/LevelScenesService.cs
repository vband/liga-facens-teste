using System.Collections.Generic;
using Code.Models.Abstraction;
using Code.Models.Concrete;
using Code.Services.Abstraction;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Services.Concrete
{
    public class LevelScenesService : ILevelScenesService
    {
        private const string LevelsPath = "Levels";

        public IReadOnlyList<ISceneModel> LevelScenes => _levels;

        public bool IsLastLevel => CurrentLevelIndex == _levels.Length - 1;
        public int CurrentLevelIndex { get; private set; }

        private readonly ISceneModel[] _levels;

        public LevelScenesService()
        {
            _levels = Resources.LoadAll<SceneModelSo>(LevelsPath);
        }
        
        public void LoadNextLevel()
        {
            if (IsLastLevel)
                return;
            
            CurrentLevelIndex++;
            RestartCurrentLevel();
        }

        public void RestartCurrentLevel()
            => SceneManager.LoadSceneAsync(_levels[CurrentLevelIndex].SceneName);

        public void LoadLevel(int levelIndex)
        {
            CurrentLevelIndex = levelIndex;
            RestartCurrentLevel();
        }
    }
}