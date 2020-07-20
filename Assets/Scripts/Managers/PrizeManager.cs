using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class PrizeManager : MonoBehaviour
    {
        public GameObject CardPrefab;
        public GameObject PrizePanel;
        public Button PrizeButton;
        public Button DiscardButton;

        public GameManager GameManager { get; set;}
        public Dictionary<Card, GameObject> Cards;

        public void Init(GameManager gameManager)
        {
            if ( Utils.isMissing("PrizeManager", new Object[]{ CardPrefab, PrizePanel, PrizeButton, DiscardButton }) ) return;

            GameManager = gameManager;
            Cards = new Dictionary<Card, GameObject>();

            PrizeButton.onClick.AddListener(delegate { OnPrizeButtonClicked(); });
            DiscardButton.onClick.AddListener(delegate { OnDiscardButtonClicked(); });

            gameManager.PrizePool.RegisterOnCardAddedCallback(OnCardAdded);
            gameManager.PrizePool.RegisterOnCardRemovedCallback(OnCardRemoved);

            PrizePanel.SetActive(false);
        }

        public void OnPrizeButtonClicked()
        {
            PrizeButton.gameObject.SetActive(false);
            PrizePanel.SetActive(true);
            GameManager.FillPrizePool();
        }

        public void OnDiscardButtonClicked()
        {
            GameManager.EmptyPrizePool();
            PrizePanel.SetActive(false);
            PrizeButton.gameObject.SetActive(true);
        }

        public void OnCardAdded(Card card)
        {
            GameObject cardObject = GameObject.Instantiate(CardPrefab);
            cardObject.transform.SetParent(PrizePanel.transform);
            cardObject.name = card.Name;

            cardObject.GetComponent<CardController>().Init(card, true);

            Cards.Add(card, cardObject);
        }

        public void OnCardRemoved(Card card)
        {
            if (!Cards.ContainsKey(card))
            {
                Debug.LogError("Attempting to remove card from prize pool lacking said card.");
                return;
            }
            GameObject cardObject = Cards[card];
            cardObject.GetComponent<CardController>().TearDown();
            Cards.Remove(card);
            GameObject.Destroy(cardObject);
        }
    }
}
