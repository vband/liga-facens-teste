using UnityEngine;

namespace Code.Behaviours.Abstraction
{
    public interface IFlyBehaviour : IBehaviour
    {
        void UpdateMovement(Vector2 axis);
    }
}