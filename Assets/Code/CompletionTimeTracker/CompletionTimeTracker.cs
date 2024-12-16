using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using TMPro;
using UnityEngine;

namespace Code.CompletionTimeTracker
{
    public class CompletionTimeTracker : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentTimeLabel;
        [SerializeField] private string _timeLabelPostfix = "s";
        
        private float _currentTime;
        private ILevelFinishedService _levelFinishedService;
        private ILevelFailedService _levelFailedService;
        private ILevelScenesService _levelScenesService;
        private ILevelModelsService _levelModelsService;
        private bool _isTracking;
        
        private void Start()
        {
            _levelFinishedService = ServiceLocator.Get<ILevelFinishedService>();
            _levelFailedService = ServiceLocator.Get<ILevelFailedService>();
            _levelScenesService = ServiceLocator.Get<ILevelScenesService>();
            _levelModelsService = ServiceLocator.Get<ILevelModelsService>();
            
            _levelFinishedService.OnLevelFinished += OnLevelFinished;
            _levelFailedService.OnLevelFailed += OnLevelFailed;
            
            StartTimer();
        }

        private void Update()
        {
            if (!_isTracking)
                return;
            
            UpdateTimer();
        }

        private void OnDestroy()
        {
            _levelFinishedService.OnLevelFinished -= OnLevelFinished;
            _levelFailedService.OnLevelFailed -= OnLevelFailed;
        }

        private void StartTimer()
        {
            _currentTime = 0;
            _isTracking = true;
        }

        private void UpdateTimer()
        {
            _currentTime += Time.deltaTime;
            _currentTimeLabel.text = _currentTime.ToString("0.00") + _timeLabelPostfix;
        }

        private void StopTimer()
            => _isTracking = false;

        private void OnLevelFinished(int _)
        {
            StopTimer();
            UpdateBestCompletionTimeIfNeeded();
        }

        private void OnLevelFailed(int _)
            => StopTimer();

        private void UpdateBestCompletionTimeIfNeeded()
        {
            var currentLevelIndex = _levelScenesService.CurrentLevelIndex;
            var currentLevelModel = _levelModelsService.GetLevelModelFromCache(currentLevelIndex);

            if (_currentTime < currentLevelModel.BestCompletionTime)
                _levelModelsService.SetBestCompletionTime(currentLevelIndex, _currentTime);
        }
    }
}