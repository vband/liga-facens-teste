using Code.Models.Abstraction;
using UnityEngine;

namespace Code.Models.Concrete
{
    [CreateAssetMenu(fileName = "New" + nameof(UnityAnalyticsConfigSo), menuName = "Create " + nameof(UnityAnalyticsConfigSo))]
    public class UnityAnalyticsConfigSo : ScriptableObject, IAnalyticsConfig
    {
        [SerializeField] private string _levelFinishedEventName = "levelFinished";
        public string LevelFinishedEventName => _levelFinishedEventName;
        
        [SerializeField] private string _levelFailedEventName = "levelFailed";
        public string LevelFailedEventName => _levelFailedEventName;

        [SerializeField] private string _levelIndexParameterName = "levelIndex";
        public string LevelIndexParameterName => _levelIndexParameterName;
    }
}