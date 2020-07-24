using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public class CardsPanelController : MonoBehaviour
    {
        public GameObject CardPrefab;
        public GameObject EquipmentPanel;
        public GameObject SpellPanel;
        public GameObject HandPanel;

        public Player Player {get; set; }
        public Dictionary<Card, GameObject> Cards;
        
        public void Init(Player player)
        {
            if ( Utils.isMissing("CardsPanelController", new Object[]{ CardPrefab, EquipmentPanel, SpellPanel, HandPanel }) ) return;
            
            Cards = new Dictionary<Card, GameObject>();

            Player = player;
            player.RegisterOnCardAddedCallback(OnCardAdded);
            player.RegisterOnCardRemovedCallback(OnCardRemoved);
        }

        public void OnCardAdded(Card card)
        {
            GameObject cardObject = GameObject.Instantiate(CardPrefab);
            cardObject.transform.SetParent(HandPanel.transform);
            cardObject.name = card.Name;

            cardObject.GetComponent<CardController>().Init(card);

            card.RegisterOnCardEquippedCallback(OnCardEquipped);
            Cards.Add(card, cardObject);
        }
        
        public void OnCardRemoved(Card card)
        {
            if (!Cards.ContainsKey(card))
            {
                Debug.LogError("Attempting to remove card from card panel lacking said card.");
                return;
            }
            GameObject cardObject = Cards[card];
            cardObject.GetComponent<CardController>().TearDown();
            card.UnregisterOnCardEquippedCallback(OnCardEquipped);
            Cards.Remove(card);
            GameObject.Destroy(cardObject);
        }

        public void OnCardEquipped(Card card)
        {   
            if (!Cards.ContainsKey(card))
            {
                Debug.LogError("Didn't unregister card equipped callback!");
                return;
            }
            if (card.IsEquipped)
            {
                if (card.IsEquipment())
                {
                    Cards[card].transform.SetParent(EquipmentPanel.transform);
                    return;
                }
                if (card.IsSpell())
                {
                    Cards[card].transform.SetParent(SpellPanel.transform);
                    return;
                }
            }
            Cards[card].transform.SetParent(HandPanel.transform);
        }
    }
}
