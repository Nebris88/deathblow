using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public enum Charge { Any_Energy, Power_Energy, Mind_Energy, Life_Energy, Monster_1, Monster_2, Monster_3 };
    
    public interface ChargeOwner 
    {
        Dictionary<Charge, int> Charges { get; set; }
        void AddCharge(Charge charge, int amount = 1);
        void RemoveCharge(Charge charge, int amount = 1);
        void ClearCharges();
        int GetCharges(Charge charge);
        void RegisterOnChargeChangedCallback(Action<Charge, ChargeOwner> callback);
        void UnregisterOnChargeChangedCallback(Action<Charge, ChargeOwner> callback);
    }
}
