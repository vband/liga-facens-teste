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
        private const string LevelModelsPath = "LevelModels.dat";
        
        private readonly ILevelScenesService _levelScenesService;

        private List<ILevelModel> _cachedLevelModels;
        private List<ILevelModel> CachedLevelModels
        {
            get
            {
                if (_cachedLevelModels != null)
                    return _cachedLevelModels;

                _cachedLevelModels = LoadLevelModelsAsync().Result;

                return _cachedLevelModels;
            }
        }

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

        public void UnlockLevel(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= CachedLevelModels.Count)
                return;
            
            if (CachedLevelModels[levelIndex].Unlocked)
                return;
            
            CachedLevelModels[levelIndex].Unlocked = true;

            SaveCachedLevelModels().Forget();
        }

        public ILevelModel GetLevelModelFromCache(int levelIndex)
            => CachedLevelModels[levelIndex];

        public void SetBestCompletionTime(int levelIndex, float bestCompletionTime)
        {
            CachedLevelModels[levelIndex].BestCompletionTime = bestCompletionTime;

            SaveCachedLevelModels().Forget();
        }

        private async Task SaveCachedLevelModels()
            => await Persistency.SaveAsync(LevelModelsPath, CachedLevelModels);
    }
}