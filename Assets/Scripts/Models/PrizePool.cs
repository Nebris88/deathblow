using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deathblow
{
    public class PrizePool : CardOwner
    {
        public List<Card> Cards { get; set; }

        private Action<Card> OnCardAddedCallback;
        private Action<Card> OnCardRemovedCallback;

        public PrizePool()
        {
            Cards = new List<Card>();
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
                Debug.LogError("Attempting to remove card from prize pool lacking said card.");
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
