using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deathblow
{
    public class Player : Entity, CardOwner
    {
        public List<Card> Cards { get; set; }

        private Action<Card> OnCardAddedCallback;
        private Action<Card> OnCardRemovedCallback;

        public Player(string name)
        {
            Name = name;
            Cards = new List<Card>();
            Dice = new List<Die>();
            Charges = new Dictionary<Charge, int>();
            Charges.Add(Charge.Power_Energy, 0);
            Charges.Add(Charge.Mind_Energy, 0);
            Charges.Add(Charge.Life_Energy, 0);
        }

        // Card Owner Method Impl
        public void AddCard(Card card)
        {
            if (card == null) return;
            Cards.Add(card);
            card.CardOwner = this; 
            if (OnCardAddedCallback != null) 
            {
                OnCardAddedCallback(card);
            }
        }

        public void RemoveCard(Card card)
        {
            if (!Cards.Contains(card))
            {
                Debug.LogError("Attempting to remove card from hand lacking said card.");
                return;
            }
            Cards.Remove(card);
            card.CardOwner = null;
            if (OnCardRemovedCallback != null) 
            {
                OnCardRemovedCallback(card);
            }
        }
        
        public void RegisterOnCardAddedCallback(Action<Card> callback)
        {
            OnCardAddedCallback += callback;
        }

        public void UnregisterOnCardAddedCallback(Action<Card> callback)
        {
            OnCardAddedCallback -= callback;
        }

        public void RegisterOnCardRemovedCallback(Action<Card> callback)
        {
            OnCardRemovedCallback += callback;
        }

        public void UnregisterOnCardRemovedCallback(Action<Card> callback)
        {
            OnCardRemovedCallback -= callback;
        }
    }
}
