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
            Debug.Log("OnDeckLoaded");
            ScrollViewContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, deck.Cards.Count * 30);

            deck.Cards.ForEach(card => {

                GameObject cardEditObject = GameObject.Instantiate(CardEditPrefab);
                cardEditObject.transform.SetParent(ScrollViewContent.transform);
                cardEditObject.name = card.Name;

                //cardEditObject.GetComponent<CardEditController>().Init(card);

                CardEdits.Add(card, cardEditObject);
            });
        }
    }
}
