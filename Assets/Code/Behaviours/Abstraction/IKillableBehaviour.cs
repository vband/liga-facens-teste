﻿using System;

namespace Code.Behaviours.Abstraction
{
    public interface IKillableBehaviour : IBehaviour
    {
        event Action OnDied;
        void Die();
    }
}