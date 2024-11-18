using System.Collections.Generic;
using Code.LevelScenes.Abstraction;
using Code.LevelScenes.Concrete;
using Code.Services.Abstraction;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Services.Concrete
{
    public class LevelScenesService : ILevelScenesService
    {
        private const string LevelsPath = "Levels";

        public IReadOnlyList<ILevelScene> LevelScenes => _levels;

        public bool IsLastLevel => _currentLevelIndex == _levels.Length - 1;

        private readonly ILevelScene[] _levels;

        private int _currentLevelIndex;
        
        public LevelScenesService()
        {
            _levels = Resources.LoadAll<LevelSceneSO>(LevelsPath);
        }
        
        public void LoadNextLevel()
        {
            if (IsLastLevel)
                return;
            
            _currentLevelIndex++;
            RestartCurrentLevel();
        }

        public void RestartCurrentLevel()
            => SceneManager.LoadSceneAsync(_levels[_currentLevelIndex].LevelSceneName);

        public void LoadLevel(int levelIndex)
        {
            _currentLevelIndex = levelIndex;
            RestartCurrentLevel();
        }

        public async void UnlockNextLevel()
        {
            if (IsLastLevel)
                return;

            var nextLevelIndex = _currentLevelIndex + 1;
            var levelModelsService = ServiceLocator.ServiceLocator.Get<ILevelModelsService>();
            
            await levelModelsService.WriteLevelUnlockedAsync(nextLevelIndex);
        }
    }
}