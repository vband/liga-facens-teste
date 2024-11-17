using Code.Levels.Abstraction;
using Code.Levels.Concrete;
using Code.Services.Abstraction;
using UnityEngine;

namespace Code.Services.Concrete
{
    public class LevelService : ILevelService
    {
        private const string LevelsPath = "Levels";

        public bool IsLastLevel => _currentLevelIndex == _levels.Length - 1;

        private readonly ILevel[] _levels;

        private int _currentLevelIndex;
        
        public LevelService()
        {
            _levels = Resources.LoadAll<LevelSO>(LevelsPath);
        }

        public void LoadFirstLevel()
        {
            _currentLevelIndex = 0;
            RestartCurrentLevel();
        }
        
        public void LoadNextLevel()
        {
            if (IsLastLevel)
                return;
            
            _levels[++_currentLevelIndex].LoadLevel();
        }

        public void RestartCurrentLevel()
            => _levels[_currentLevelIndex].LoadLevel();
    }
}