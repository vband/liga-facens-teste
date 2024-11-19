using Cinemachine;
using Code.Actors.Concrete;
using UnityEngine;

namespace Code.Cinemachine
{
    public class DisattachTargetFollowOnPlayerDeath : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        
        private void Start()
        {
            var playerActor = FindObjectOfType<PlayerActor>();

            if (playerActor == null)
                return;

            playerActor.KillableBehaviour.OnDied += OnPlayerDied;
        }

        private void OnPlayerDied()
            => _cinemachineVirtualCamera.Follow = null;
    }
}