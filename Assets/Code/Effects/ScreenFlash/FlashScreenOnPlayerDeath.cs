using Code.Actors.Concrete;
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

            playerActor.KillableBehaviour.OnDied += FlashScreen;
        }

        private void FlashScreen()
            => _screenFlash.FlashScreen();
    }
}