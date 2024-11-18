using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Models.Abstraction;
using Code.Models.Concrete;
using Code.Services.Abstraction;
using Code.Utils;

namespace Code.Services.Concrete
{
    public class LevelModelsService : ILevelModelsService
    {
        private const string LevelModelsPath = "LevelModels";
        
        private readonly ILevelScenesService _levelScenesService;

        private List<ILevelModel> _cachedLevelModels;

        public LevelModelsService(ILevelScenesService levelScenesService)
        {
            _levelScenesService = levelScenesService;
        }
        
        public async Task<List<ILevelModel>> LoadLevelModelsAsync()
        {
            if (_cachedLevelModels != null)
                return _cachedLevelModels;
            
            var (success, levelModels) = await Persistency.TryLoadAsync<List<ILevelModel>>(LevelModelsPath);

            _cachedLevelModels = success ? levelModels : CreateLevelModels();
            
            return _cachedLevelModels;
        }
        
        private List<ILevelModel> CreateLevelModels()
        {
            var levelScenes = _levelScenesService.LevelScenes;
            var levelModels = new List<ILevelModel>();

            for (var i = 0; i < levelScenes.Count; i++)
            {
                var levelModel = new LevelModel((i + 1).ToString(), i == 0, i);
                levelModels.Add(levelModel);
            }

            return levelModels;
        }

        public async Task WriteLevelUnlockedAsync(int levelIndex)
        {
            if (_cachedLevelModels[levelIndex].Unlocked)
                return;
            
            _cachedLevelModels[levelIndex].Unlocked = true;

            await Persistency.SaveAsync(LevelModelsPath, _cachedLevelModels);
        }
    }
}