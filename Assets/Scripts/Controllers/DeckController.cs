using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class DeckController : MonoBehaviour
    {
        public Button DrawButton;
        public Text CardCount;
        public Text InDeckCount;
        public Text InDiscardCount;
        public Text InUseCount;

        public Deck Deck { get; set; }

        public void Init(GameManager gameManager)
        {
            if ( Utils.isMissing("DeckController", new Object[]{ DrawButton, CardCount, InDeckCount, InDiscardCount, InUseCount }) ) return;
            Deck = gameManager.Deck;
            DrawButton.onClick.AddListener(delegate { gameManager.ActivePlayer.AddCard(Deck.Draw()); });
        }

        void Update()
        {
            if (Deck != null)
            {
                CardCount.text = Deck.Cards.Count.ToString();
                InDeckCount.text = Deck.GetCardsInDeck().ToString();
                InDiscardCount.text = Deck.GetCardsInDiscard().ToString();
                InUseCount.text = (Deck.Cards.Count - Deck.GetCardsInDeck() - Deck.GetCardsInDiscard()).ToString();
            }
        }
    }
}
