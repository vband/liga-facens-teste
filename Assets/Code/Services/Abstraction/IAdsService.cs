using System;

namespace Code.Services.Abstraction
{
    public interface IAdsService : IService
    {
        void DoAfterInterstitialAd(Action afterAd);
    }
}