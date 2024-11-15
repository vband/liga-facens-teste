using UnityEngine;

namespace Code.Actors.Concrete
{
    public abstract class Rigidbody2DActor : BaseActor
    {
        [SerializeField] protected Rigidbody2D _rigidbody2D;
    }
}