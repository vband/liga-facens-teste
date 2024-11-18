using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Models.Abstraction;

namespace Code.Services.Abstraction
{
    public interface ILevelModelsService : IService
    {
        Task<List<ILevelModel>> LoadLevelModelsAsync();
        void UnlockLevel(int levelIndex);
        ILevelModel GetLevelModelFromCache(int levelIndex);
        void SetBestCompletionTime(int levelIndex, float bestCompletionTime);
    }
}