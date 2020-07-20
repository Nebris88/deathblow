using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public class Card
    {
        private bool _IsEquipped;

        public CardOwner CardOwner { get; set; }
        public Deck Deck { get; set; }
        public string Name { get; set; }
        public List<DieFace> Bonuses { get; set; }
        public List<Charge> Costs { get; set; }
        
        public bool IsEquipment { get; set; }
        public bool IsSpell { get; set; }

        public bool IsEquipped 
        { 
            get => _IsEquipped; 
            set 
            {
                bool change = (_IsEquipped != value);
                _IsEquipped = value;
                if (change && OnCardEquippedCallback != null)
                {
                    OnCardEquippedCallback(this);
                }
            } 
        }

        /*
        private CardOwner _CardOwner;
        public CardOwner CardOwner
        {
            get => _CardOwner; 
            set 
            {
                bool change = (_CardOwner != value);
                if (change && _CardOwner != null)
                {
                    _CardOwner.RemoveCard(this);
                }
                _CardOwner = value;
            } 
        }
        */

        Action<Card> OnCardEquippedCallback;

        public Card (Deck deck, string name)
        {
            Deck = deck;
            Name = name;
            IsEquipment = false;
            IsSpell = false;
            Bonuses = new List<DieFace>();
            Costs = new List<Charge>();

            RandomizeCard();
        }

        public void DiscardToDeck()
        {
            Deck.AddToDiscard(this);
        }

        public Card LeaveOwner()
        {
            if (CardOwner != null)
            {
                IsEquipped = false;
                CardOwner.RemoveCard(this);
            }
            return this;
        }

        public void RegisterOnCardEquippedCallback(Action<Card> callback)
        {
            OnCardEquippedCallback += callback;
        }

        public void UnregisterOnCardEquippedCallback(Action<Card> callback)
        {
            OnCardEquippedCallback -= callback;
        }

        public bool IsEquippable()
        {
            return IsEquipment || IsSpell;
        }

        public void RandomizeCard()
        {
            int type = (int)UnityEngine.Random.Range(0, 3);

            if (type == 0)
            {
                IsEquipment = true;
                for (int x = 0; x < (int)UnityEngine.Random.Range(1, 4); x++)
                {
                    Bonuses.Add(Dice.Roll(DieType.Standard));
                }
                return;
            }
            if (type == 1)
            {
                IsSpell = true;
                for (int x = 0; x < (int)UnityEngine.Random.Range(1, 4); x++)
                {
                    Costs.Add((Charge)(int)UnityEngine.Random.Range(0, 4));
                }
            }
        }
    }
}