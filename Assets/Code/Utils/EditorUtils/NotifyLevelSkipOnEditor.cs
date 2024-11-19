using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using UnityEngine;

namespace Code.Utils.EditorUtils
{
    public class NotifyLevelSkipOnEditor : MonoBehaviour
    {
        
#if UNITY_EDITOR
        
        [SerializeField] private int _thisLevelIndex;

        private void Start()
        {
            var levelScenesService = ServiceLocator.Get<ILevelScenesService>();
            levelScenesService.NotifyLevelSkip(_thisLevelIndex);
        }
        
#endif
        
    }
}