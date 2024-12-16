using System;
using UnityEngine;

namespace Code.Utils
{
    public class CollisionObserver : MonoBehaviour
    {
        public event Action<GameObject> OnCollisionEnter; 
        public event Action<GameObject> OnCollisionExit;
        public event Action<GameObject> OnTriggerEnter; 
        public event Action<GameObject> OnTriggerExit;

        private void OnCollisionEnter2D(Collision2D other)
            => OnCollisionEnter?.Invoke(other.gameObject);

        private void OnCollisionExit2D(Collision2D other)
            => OnCollisionExit?.Invoke(other.gameObject);

        private void OnTriggerEnter2D(Collider2D other)
            => OnTriggerEnter?.Invoke(other.gameObject);

        private void OnTriggerExit2D(Collider2D other)
            => OnTriggerExit?.Invoke(other.gameObject);
    }
}