using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public class Monster// : CardOwner, DieOwner, ChargeOwner
    {
        /*
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
        public List<Die> Dice { get; set; }
        public Dictionary<Charge, int> Charges { get; set; }

        Action<Card> OnHandChangedCallback;
        Action<Die> OnDiceChangedCallback;
        Action<Charge, ChargeOwner> OnChargeChangedCallback;

        public Monster(string name)
        {
            Name = name;
            Cards = new List<Card>();
            Dice = new List<Die>();
            Charges = new Dictionary<Charge, int>();
            Charges.Add(Charge.Power, 0);
            Charges.Add(Charge.Mind, 0);
            Charges.Add(Charge.Life, 0);
        }

        // Card Owner Method Impl
        public void AddCard(Card card)
        {
            Cards.Add(card);
            card.CardOwner = this; 
            if (OnHandChangedCallback != null) 
            {
                OnHandChangedCallback(card);
            }
        }

        public void RemoveCard(Card card)
        {
            if (Cards.Contains(card))
            {
                Debug.LogError("Attempting to remove card from hand lacking said card.");
                return;
            }
            Cards.Remove(card);
            card.CardOwner = null;
            if (OnHandChangedCallback != null) 
            {
                OnHandChangedCallback(card);
            }
        }
        
        public void RegisterOnHandChangedCallback(Action<Card> callback)
        {
            OnHandChangedCallback += callback;
        }

        public void UnregisterOnHandChangedCallback(Action<Card> callback)
        {
            OnHandChangedCallback -= callback;
        }

        // Die Owner Method Impl
        public void AddDie(Die die)
        {
            Dice.Add(die);
            if (OnDiceChangedCallback != null) 
            {
                OnDiceChangedCallback(die);
            }
        }

        public void RemoveDie(Die die)
        {
            if (Dice.Contains(die))
            {
                Debug.LogError("Attempting to remove die from player lacking said die.");
                return;
            }
            Dice.Remove(die);
            if (OnDiceChangedCallback != null) 
            {
                OnDiceChangedCallback(die);
            }
        }

        public void RollDice(bool rollLocked = false, bool rollFrozen = false)
        {
            Dice.ForEach(die => {
                if ((!die.IsFrozen || rollFrozen) && (!die.IsLocked || rollLocked) )
                {
                    die.Roll();
                if (OnDiceChangedCallback != null) 
                {
                    OnDiceChangedCallback(die);
                }
                }
            });
            UnlockDice();
        }

        public void UnfreezeDice()
        {
            Dice.ForEach(die => {
                die.IsFrozen = false;
                if (OnDiceChangedCallback != null) 
                {
                    OnDiceChangedCallback(die);
                }
            });
        }

        public void UnlockDice()
        {
            Dice.ForEach(die => {
                die.IsLocked = false;
                if (OnDiceChangedCallback != null) 
                {
                    OnDiceChangedCallback(die);
                }
            });
        }
        
        public void RegisterOnDiceChangedCallback(Action<Die> callback)
        {
            OnDiceChangedCallback += callback;
        }

        public void UnregisterOnDiceChangedCallback(Action<Die> callback)
        {
            OnDiceChangedCallback -= callback;
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
            List<Charge> ChargeKeys = new List<Charge>(Charges.Keys);
            ChargeKeys.ForEach(charge => {
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
        */
    }
}
