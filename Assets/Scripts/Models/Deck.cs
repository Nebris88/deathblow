using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deathblow
{
    public class Deck
    {
        public List<Card> Cards { get; set; }
        private Stack<Card> deck = new Stack<Card>();
        private Stack<Card> discard = new Stack<Card>();

        Action<Card> OnCardAddedCallback;
        Action<Card> OnCardRemovedCallback;

        public Deck()
        {
            Cards = new List<Card>();
            for (int x = 1; x < 61; x++)
            {
                Cards.Add(new Card(this, "" + x));
            }
            ReturnAndShuffle();
        }

        public Deck(DeckData deckData)
        {
            Cards = new List<Card>();
            foreach (CardData cardData in deckData.cards)
            {
                Cards.Add(new Card(this, cardData));
            }
            ReturnAndShuffle();
        }

        public bool CardsInDeck()
        {
            return deck.Count > 0;
        }

        public bool CardsInDiscard()
        {
            return discard.Count > 0;
        }

        public int GetCardsInDeck()
        {
            return deck.Count;
        }

        public int GetCardsInDiscard()
        {
            return discard.Count;
        }

        public bool CardsInDeckOrDiscard()
        {
            return CardsInDeck() || CardsInDiscard();
        }

        public Card Draw()
        {
            return Draw(1).FirstOrDefault();
        }

        public List<Card> Draw(int number)
        {
            List<Card> drawnCards = new List<Card>();
            while (drawnCards.Count < number && CardsInDeckOrDiscard())
            {
                while (drawnCards.Count < number && CardsInDeck())
                {
                    drawnCards.Add(deck.Pop());
                }
                if (!CardsInDeck() && CardsInDiscard())
                {
                    ShuffleDiscardIntoDeck();
                }
            }
            return drawnCards;
        }

        public void AddToDiscard(Card card)
        {
            discard.Push(card.LeaveOwner());
        }

        public void AddToDiscard(List<Card> discardedCards)
        {
            discardedCards.ForEach(card => {
                discard.Push(card.LeaveOwner());
            });
        }

        public void ShuffleDeck()
        {
            Card[] deckCards = deck.ToArray();
            for (int i = 0; i < deckCards.Length; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, deckCards.Length);
                Card temp = deckCards[randomIndex];
                deckCards[randomIndex] = deckCards[i];
                deckCards[i] = temp;
            }
            deck.Clear();
            deckCards.ToList().ForEach(card => {
                deck.Push(card);
            });
        }

        public void ReturnCardsToDeck()
        {
            deck.Clear();
            discard.Clear();
            Cards.ForEach(card => {
                deck.Push(card.LeaveOwner());
            });
        }

        public void ReturnAndShuffle()
        {
            ReturnCardsToDeck();
            ShuffleDeck();
        }

        public void ShuffleDiscardIntoDeck()
        {
            discard.ToList().ForEach(card => {
                deck.Push(card);
            });
            discard.Clear();
            ShuffleDeck();
        }

        public void Deal(List<CardOwner> cardOwners, int num)
        {
            for (int x = 0; x < num; x++)
            {
                cardOwners.ForEach(owner => {
                    owner.AddCard(Draw());
                });
            }
        }

        public static void PrintCards(Card card, string prefix = "")
        {
            List<Card> cardList = new List<Card>();
            cardList.Add(card);
            PrintCards(cardList, prefix);
        }

        public static void PrintCards(List<Card> cardList, string prefix = "")
        {
            if(cardList == null) return;
            string cardString = prefix + " | ";
            cardList.ForEach(card => {
                cardString += card.Name + ", ";
            });
            Debug.Log(cardString);
        }

        public void PrintDeck()
        {
            PrintCards(deck.ToList());
        }

        public void AddCard()
        {
            Card newCard = new Card(this, "New Card");
            Cards.Add(newCard);

            if (OnCardAddedCallback != null)
            {
                OnCardAddedCallback(newCard);
            }
        }

        public void RemoveCard(Card card)
        {
            if (!Cards.Contains(card))
            {
                Debug.LogError("Trying to remove card from deck that isn't in deck");
                return;
            }

            Cards.Remove(card);

            if (OnCardRemovedCallback != null)
            {
                OnCardRemovedCallback(card);
            }
        }

        public void SortDeck()
        {
            Debug.Log("Sorting deck.");
            Cards.ForEach(card => { card.SortStats(); });
            Cards = Cards.OrderBy(card => card.CardType).ThenBy(card => card.EquipmentType).ToList();
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


    [Serializable]
    public class DeckData
    {
        public CardData[] cards;
        
        public DeckData(Deck deck)
        {
            cards = deck.Cards.ConvertAll(card => new CardData(card)).ToArray();
        }
    }
}