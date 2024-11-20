using System;
using Code.Behaviours.Abstraction;

namespace Code.Actors.Abstraction
{
    public interface IActor : IDisposable
    {
        bool TryAddBehaviour<T>(T behaviour) where T : IBehaviour;
        bool TryGetBehaviour<T>(out T behaviour) where T : IBehaviour;
    }
}