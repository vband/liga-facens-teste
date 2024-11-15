using Code.Behaviours.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Concrete
{
    public class GroundCheckBehaviour : IGroundCheckBehaviour
    {
        private const string GroundLayerName = "Ground";
        
        public bool IsGrounded { get; private set; }
        
        public void OnNewContact(GameObject other)
        {
            var groundLayer = LayerMask.NameToLayer(GroundLayerName);
            
            if (!SameLayer(groundLayer, other.layer))
                return;
            
            IsGrounded = true;
        }

        public void OnLoseContact(GameObject other)
        {
            var groundLayer = LayerMask.NameToLayer(GroundLayerName);
            
            if (!SameLayer(groundLayer, other.layer))
                return;
            
            IsGrounded = false;
        }

        private static bool SameLayer(int layer1, int layer2)
            => (layer1 & layer2) > 0;
    }
}