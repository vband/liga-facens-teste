using Code.Actors.Concrete;
using Code.Behaviours.Abstraction;
using UnityEngine;

namespace Code.Effects.ScreenFlash
{
    public class FlashScreenOnPlayerDeath : MonoBehaviour
    {
        [SerializeField] private ScreenFlash _screenFlash;
        
        private void Start()
        {
            var playerActor = FindObjectOfType<PlayerActor>();

            if (playerActor == null)
                return;

            if (!playerActor.TryGetBehaviour<IKillableBehaviour>(out var killableBehaviour))
                return;

            killableBehaviour.OnDied += FlashScreen;
        }

        private void FlashScreen()
            => _screenFlash.FlashScreen();
    }
}