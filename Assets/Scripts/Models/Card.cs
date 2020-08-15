using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deathblow
{
    public enum CardType { Item, Equipment, Spell, Other }
    public enum EquipmentType { Unequppable, Weapon, Headgear, Body, Accessory, Free }
    public enum CardBonus { Die, Attack, Defense, Heal, Power, Mind, Life }  

    public class Card
    {
        private bool _IsEquipped;

        public CardOwner CardOwner { get; set; }
        public Deck Deck { get; set; }
        public string Name { get; set; }
        public string Effect { get; set; }
        public CardType CardType { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public List<CardBonus> Bonuses { get; set; }
        public List<Charge> Costs { get; set; }

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
            Effect = "";
            Bonuses = new List<CardBonus>();
            Costs = new List<Charge>();

            RandomizeCard();
            RegisterOnCardEquippedCallback(OnThisEquipped);
        }

        public Card (Deck deck, CardData cardData)
        {
            Bonuses = new List<CardBonus>();
            Costs = new List<Charge>();

            Deck = deck;
            Name = cardData.name;
            Effect = cardData.effect;

            try
            {
                CardType = (CardType)Enum.Parse(typeof(CardType), cardData.cardType);
                EquipmentType = (EquipmentType)Enum.Parse(typeof(EquipmentType), cardData.equipmentType);
                foreach (string bonus in cardData.bonuses) Bonuses.Add((CardBonus)Enum.Parse(typeof(CardBonus), bonus));
                foreach (string cost in cardData.costs) Costs.Add((Charge)Enum.Parse(typeof(Charge), cost));
            }
            catch (ArgumentException e)
            {
                Debug.LogError(e);
            }

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
            return !(CardType == CardType.Item);
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

        public bool IsEquipment()
        {
            return CardType == CardType.Equipment;
        }

        public bool IsSpell()
        {
            return CardType == CardType.Spell;
        }

        public void RandomizeCard()
        {
            CardType = (CardType)(int)UnityEngine.Random.Range(0, 3);

            if (CardType == CardType.Equipment)
            {
                EquipmentType = (EquipmentType)(int)UnityEngine.Random.Range(1, 5);
                for (int x = 0; x < (int)UnityEngine.Random.Range(1, 4); x++)
                {
                    Bonuses.Add((CardBonus)(int)UnityEngine.Random.Range(0, 7));
                }
                return;
            }
            if (CardType == CardType.Spell)
            {
                for (int x = 0; x < (int)UnityEngine.Random.Range(1, 4); x++)
                {
                    Costs.Add((Charge)(int)UnityEngine.Random.Range(0, 4));
                }
            }
        }
    }

    [Serializable]
    public class CardData
    {
        public string name;
        public string effect;
        public string cardType;
        public string equipmentType;
        public string[] bonuses;
        public string[] costs;

        public CardData(Card card)
        {
            name = card.Name;
            effect = card.Effect;
            cardType = card.CardType.ToString();
            equipmentType = card.EquipmentType.ToString();
            bonuses = card.Bonuses.ConvertAll(bonus => bonus.ToString()).ToArray();
            costs = card.Costs.ConvertAll(cost => cost.ToString()).ToArray();
        }
    }
}