using UnityEngine;

namespace Code.Actors.Abstraction
{
    public interface IFlyerActor
    {
        void UpdateMovement(Vector2 delta);
    }
}