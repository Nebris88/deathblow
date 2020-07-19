using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deathblow
{
    public abstract class Entity : DieOwner, ChargeOwner
    {
        public string Name { get; set; }
        public List<Die> Dice { get; set; }
        public Dictionary<Charge, int> Charges { get; set; }

        private Action<Die> OnDieAddedCallback;
        private Action<Die> OnDieRemovedCallback;
        private Action<Charge, ChargeOwner> OnChargeChangedCallback;

        // Die Owner Method Impl
        public void AddDie(Die die)
        {
            Dice.Add(die);
            if (OnDieAddedCallback != null) 
            {
                OnDieAddedCallback(die);
            }
        }

        public void RemoveDie(Die die)
        {
            if (!Dice.Contains(die))
            {
                Debug.LogError("Attempting to remove die from player lacking said die.");
                return;
            }
            Dice.Remove(die);
            if (OnDieRemovedCallback != null) 
            {
                OnDieRemovedCallback(die);
            }
        }

        public void RemoveDice()
        {
            new List<Die>(Dice).ForEach(die => {
                RemoveDie(die);
            });
        }

        public void RollDice(bool rollLocked = false, bool rollFrozen = false)
        {
            Dice.ForEach(die => {
                if ((!die.IsFrozen || rollFrozen) && (!die.IsLocked || rollLocked) )
                {
                    die.Roll();
                }
            });
            UnlockDice();
        }

        public void UnfreezeDice()
        {
            Dice.ForEach(die => {
                die.IsFrozen = false;
            });
        }

        public void UnlockDice()
        {
            Dice.ForEach(die => {
                die.IsLocked = false;
            });
        }

        
        public void RegisterOnDieAddedCallback(Action<Die> callback)
        {
            OnDieAddedCallback += callback;
        }

        public void UnregisterOnDieAddedCallback(Action<Die> callback)
        {
            OnDieAddedCallback -= callback;
        }
        
        public void RegisterOnDieRemovedCallback(Action<Die> callback)
        {
            OnDieRemovedCallback += callback;
        }
        
        public void UnregisterOnDieRemovedCallback(Action<Die> callback)
        {
            OnDieRemovedCallback += callback;
        }

        // Charge Owner Method Impl
        public void AddCharge(Charge charge, int amount = 1)
        {
            if (!Charges.ContainsKey(charge))
            {
                Debug.LogError("Attempting to add charge from player lacking said capacity to hold charge: " + charge);
                return;
            }
            Charges[charge] += amount;
            Charges[charge] = Math.Max(Charges[charge], 0); //incase some asshole tries to pass a negative number...
            if (OnChargeChangedCallback != null) 
            {
                OnChargeChangedCallback(charge, this);
            }
        }

        public void RemoveCharge(Charge charge, int amount = 1)
        {
            if (!Charges.ContainsKey(charge))
            {
                Debug.LogError("Attempting to remove charge from player lacking said capacity to hold charge: " + charge);
                return;
            }
            Charges[charge] -= amount;
            Charges[charge] = Math.Max(Charges[charge], 0);
            if (OnChargeChangedCallback != null) 
            {
                OnChargeChangedCallback(charge, this);
            }
        }

        public void ClearCharges()
        {
            Charges.Keys.ToList().ForEach(charge => {
                Charges[charge] = 0;
                if (OnChargeChangedCallback != null) 
                {
                    OnChargeChangedCallback(charge, this);
                }
            });
        }

        public int GetCharges(Charge charge)
        {
            if (!Charges.ContainsKey(charge))
            {
                Debug.LogError("Attempting to get charges from player lacking said capacity to hold charge: " + charge);
                return 0;
            }
            return Charges[charge];
        }

        public void RegisterOnChargeChangedCallback(Action<Charge, ChargeOwner> callback)
        {
            OnChargeChangedCallback += callback;
        }

        public void UnregisterOnChargeChangedCallback(Action<Charge, ChargeOwner> callback)
        {
            OnChargeChangedCallback += callback;
        }
    }
}
