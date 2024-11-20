using Cinemachine;
using Code.Actors.Concrete;
using Code.Behaviours.Abstraction;
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

            if (!playerActor.TryGetBehaviour<IKillableBehaviour>(out var killableBehaviour))
                return;

            killableBehaviour.OnDied += OnPlayerDied;
        }

        private void OnPlayerDied()
            => _cinemachineVirtualCamera.Follow = null;
    }
}