using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deathblow
{
    public enum CardBonus { Die, Attack, Defense, Heal, Power, Mind, Life }  

    public class Card
    {
        private bool _IsEquipped;

        public CardOwner CardOwner { get; set; }
        public Deck Deck { get; set; }
        public string Name { get; set; }
        public List<CardBonus> Bonuses { get; set; }
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
            Bonuses = new List<CardBonus>();
            Costs = new List<Charge>();

            RandomizeCard();
            RegisterOnCardEquippedCallback(OnThisEquipped);
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

        public void OnThisEquipped(Card card)
        {
            if (card.CardOwner != null && card.CardOwner is DieOwner)
            {
                DieOwner dieOwner = (DieOwner) card.CardOwner;
                Bonuses.Where(bonus => bonus == CardBonus.Die).ToList().ForEach(bonus => {
                    if (IsEquipped)
                    {
                        dieOwner.AddDie(new Die(dieOwner, DieType.Standard));
                    }
                    else
                    {
                        List<Die> dice = dieOwner.Dice;
                        if (dice.Count > 0)
                        {
                            dieOwner.RemoveDie(dice[0]);
                        }
                    }
                });
            }
        }

        public void RandomizeCard()
        {
            int type = (int)UnityEngine.Random.Range(0, 3);

            if (type == 0)
            {
                IsEquipment = true;
                for (int x = 0; x < (int)UnityEngine.Random.Range(1, 4); x++)
                {
                    Bonuses.Add((CardBonus)(int)UnityEngine.Random.Range(0, 7));
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