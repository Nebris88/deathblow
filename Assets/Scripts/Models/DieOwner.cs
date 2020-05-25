using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public interface DieOwner 
    {
        List<Die> Dice { get; set; }
        void AddDie(Die die);
        void RemoveDie(Die die);
        void RemoveDice();
        void RollDice(bool rollLocked = false, bool rollFrozen = false);
        void UnfreezeDice();
        void UnlockDice();
        void RegisterOnDieAddedCallback(Action<Die> callback);
        void UnregisterOnDieAddedCallback(Action<Die> callback);
        void RegisterOnDieRemovedCallback(Action<Die> callback);
        void UnregisterOnDieRemovedCallback(Action<Die> callback);
    }
}
