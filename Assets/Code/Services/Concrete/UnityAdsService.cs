using System;
using Code.Services.Abstraction;

namespace Code.Services.Concrete
{
    public class UnityAdsService : IAdsService
    {
        public void DoAfterInterstitialAd(Action afterAd)
        {
            throw new NotImplementedException();
        }
    }
}