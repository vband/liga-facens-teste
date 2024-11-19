using UnityEngine;

namespace Code.Effects.ScreenFlash
{
    public class ScreenFlash : MonoBehaviour
    {
        private readonly int _screenFlashAnimation = Animator.StringToHash("ScreenFlash");

        [SerializeField] private Animator _animator;

        public void FlashScreen()
            => _animator.Play(_screenFlashAnimation);
    }
}
