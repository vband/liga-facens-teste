using System;
using Code.Services.Abstraction;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Code.Services.Concrete
{
    public class UnityAdsService : IAdsService, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string GameId = "1244935";
        private const string videoPlacement = "video";

        private bool _initializionComplete;
        private Action _afterAdCallback;

        public UnityAdsService()
        {
            Initialize();
        }
        
        public void DoAfterInterstitialAd(Action afterAd)
        {
            if (!_initializionComplete)
            {
                afterAd.Invoke();
                return;
            }
            
            LoadNonRewardedAd();
            _afterAdCallback = afterAd;
        }
        
        private void Initialize()
        {
            if (!Advertisement.isSupported)
            {
                Debug.LogError($"{Application.platform} is not supported by Advertisement.");
                return;
            }
            
            Advertisement.Initialize(GameId, true, this);
        }
        
        private void LoadNonRewardedAd()
            => Advertisement.Load(videoPlacement, this);

        private void ShowNonRewardedAd()
            => Advertisement.Show(videoPlacement, this);

        public void OnInitializationComplete()
        {
            _initializionComplete = true;
            Debug.Log("Advertisement Initialization complete");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"{nameof(UnityAdsService)} Initialization failed." +
                      $"\nError: {error}." +
                      $"\nMessage: {message}");
        }

        public void OnUnityAdsAdLoaded(string placementId)
            => ShowNonRewardedAd();

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"{nameof(UnityAdsService)} Failed to load ad." +
                      $"\nError: {error}." +
                      $"\nMessage: {message}");
            
            _afterAdCallback.Invoke();
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log($"{nameof(UnityAdsService)} Failed to show ad." +
                      $"\nError: {error}." +
                      $"\nMessage: {message}");
            
            _afterAdCallback.Invoke();
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            // Intentionally left blank
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            // Intentionally left blank
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
            => _afterAdCallback.Invoke();
    }
}