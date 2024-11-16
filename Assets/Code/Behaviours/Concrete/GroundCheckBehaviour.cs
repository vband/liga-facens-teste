using Code.Behaviours.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Concrete
{
    public class GroundCheckBehaviour : IGroundCheckBehaviour
    {
        private static readonly int _groundLayer = LayerMask.NameToLayer("Ground");
        
        public bool IsGrounded { get; private set; }
        
        public void OnNewContact(GameObject other)
        {
            if (!SameLayer(_groundLayer, other.layer))
                return;
            
            IsGrounded = true;
        }

        public void OnLoseContact(GameObject other)
        {
            if (!SameLayer(_groundLayer, other.layer))
                return;
            
            IsGrounded = false;
        }

        private static bool SameLayer(int layer1, int layer2)
            => (layer1 & layer2) > 0;
    }
}