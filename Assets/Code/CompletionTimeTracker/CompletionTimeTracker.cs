using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using UnityEngine;

namespace Code.CompletionTimeTracker
{
    public class CompletionTimeTracker : MonoBehaviour
    {
        private float _currentTime;
        private ILevelFinishedService _levelFinishedService;
        private ILevelScenesService _levelScenesService;
        private ILevelModelsService _levelModelsService;
        
        private void Start()
        {
            _levelFinishedService = ServiceLocator.Get<ILevelFinishedService>();
            _levelScenesService = ServiceLocator.Get<ILevelScenesService>();
            _levelModelsService = ServiceLocator.Get<ILevelModelsService>();
            
            _levelFinishedService.OnLevelFinishedInvoked += UpdateBestCompletionTimeIfNeeded;
            
            StartTimer();
        }

        private void Update()
        {
            UpdateTimer();
        }

        private void OnDestroy()
        {
            _levelFinishedService.OnLevelFinishedInvoked -= UpdateBestCompletionTimeIfNeeded;
        }

        private void StartTimer()
            => _currentTime = 0;

        private void UpdateTimer()
            => _currentTime += Time.deltaTime;

        private void UpdateBestCompletionTimeIfNeeded()
        {
            var currentLevelIndex = _levelScenesService.CurrentLevelIndex;
            var currentLevelModel = _levelModelsService.GetLevelModelFromCache(currentLevelIndex);

            if (_currentTime < currentLevelModel.BestCompletionTime)
                _levelModelsService.SetBestCompletionTime(currentLevelIndex, _currentTime);
        }
    }
}