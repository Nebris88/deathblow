using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public interface CardOwner 
    {
        List<Card> Cards { get; set; }
        void AddCard(Card card);
        void RemoveCard(Card card);
        void RegisterOnCardAddedCallback(Action<Card> callback);
        void UnregisterOnCardAddedCallback(Action<Card> callback);
        void RegisterOnCardRemovedCallback(Action<Card> callback);
        void UnregisterOnCardRemovedCallback(Action<Card> callback);
    }
}
