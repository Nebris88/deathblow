using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deathblow
{
    public class DeckEditController : MonoBehaviour
    {
        public GameObject ScrollViewContent;
        public GameObject CardEditPrefab;

        public Dictionary<Card, GameObject> CardEdits;

        public void Init(DeckSceneManager deckSceneManager)
        {
            CardEdits = new Dictionary<Card, GameObject>();

            if ( Utils.isMissing("DeckEditController", new UnityEngine.Object[]{ ScrollViewContent, CardEditPrefab }) ) return;

            deckSceneManager.RegisterOnDeckLoadedCallback(OnDeckLoaded);
        }

        public void OnDeckLoaded(Deck deck)
        {
            new List<Card>(CardEdits.Keys).ForEach(card => {
                DeleteCard(card);
            });

            deck.Cards.ForEach(card => {
                AddCard(card);
            });

            deck.RegisterOnCardAddedCallback(AddCard);
            deck.RegisterOnCardRemovedCallback(DeleteCard);
        }

        public void AddCard(Card card)
        {
            GameObject cardEditObject = GameObject.Instantiate(CardEditPrefab);
            cardEditObject.transform.SetParent(ScrollViewContent.transform);
            cardEditObject.name = card.Name;

            cardEditObject.GetComponent<CardEditController>().Init(card);

            CardEdits.Add(card, cardEditObject);
        }

        public void DeleteCard(Card card)
        {
            if (!CardEdits.ContainsKey(card))
            {
                Debug.LogError("Trying to delete card that shouldn't exist!");
                return;
            }

            GameObject cardObject = CardEdits[card];
            CardEdits.Remove(card);
            GameObject.Destroy(cardObject);
        }
    }
}
