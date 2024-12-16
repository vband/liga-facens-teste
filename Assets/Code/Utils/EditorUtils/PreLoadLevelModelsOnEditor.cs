using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using UnityEngine;

namespace Code.Utils.EditorUtils
{
    public class PreLoadLevelModelsOnEditor : MonoBehaviour
    {
#if UNITY_EDITOR

        private void Start()
        {
            var levelModelsService = ServiceLocator.Get<ILevelModelsService>();
            levelModelsService.LoadLevelModelsAsync();
        }
        
#endif
    }
}