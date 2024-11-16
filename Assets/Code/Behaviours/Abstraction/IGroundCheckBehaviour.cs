using UnityEngine;

namespace Code.Behaviours.Abstraction
{
    public interface IGroundCheckBehaviour : IBehaviour
    {
        bool IsGrounded { get; }
        void OnNewContact(GameObject other);
        public void OnLoseContact(GameObject other);
    }
}