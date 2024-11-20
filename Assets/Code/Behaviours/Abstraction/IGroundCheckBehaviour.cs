using System;
using UnityEngine;

namespace Code.Behaviours.Abstraction
{
    public interface IGroundCheckBehaviour : IBehaviour
    {
        event Action<bool> OnGroundedStateChanged;
        bool IsGrounded { get; }
        void OnNewContact(GameObject other);
        public void OnLoseContact(GameObject other);
    }
}