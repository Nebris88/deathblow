using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class CardController : MonoBehaviour
    {
        public Button SelectButton;
        public Text CardName;
        public Toggle EquipToggle;
        public Button DiscardButton;
        public GameObject CardIconPanel; 
        public GameObject CardIconPrefab;

        public Card Card { get; set; }

        public void Init(Card card, bool selectable = false)
        {
            if ( Utils.isMissing("CardController", new Object[]{ SelectButton, CardName, EquipToggle, DiscardButton, CardIconPanel, CardIconPrefab }) ) return;

            Card = card;
            
            CardName.text = card.Name;
            EquipToggle.onValueChanged.AddListener(delegate { card.IsEquipped = EquipToggle.isOn; });
            DiscardButton.onClick.AddListener(delegate { card.DiscardToDeck(); });
            EquipToggle.gameObject.SetActive(card.IsEquippable() && !selectable);

            SelectButton.interactable = selectable;
            SelectButton.onClick.AddListener(delegate { OnCardSelected(); });

            if (card.IsEquipment)
            {   
                card.Bonuses.ForEach(bonus => {
                    GameObject cardIconObject = GameObject.Instantiate(CardIconPrefab);
                    cardIconObject.transform.SetParent(CardIconPanel.transform);
                    cardIconObject.name = card.Name;
                    cardIconObject.GetComponent<Image>().sprite = MasterManager.Instance.ResourceManager.GetSpriteByCardBonus(bonus);
                });
            }

            if (card.IsSpell)
            {   
                card.Costs.ForEach(charge => {
                    GameObject cardIconObject = GameObject.Instantiate(CardIconPrefab);
                    cardIconObject.transform.SetParent(CardIconPanel.transform);
                    cardIconObject.name = card.Name;
                    cardIconObject.GetComponent<Image>().sprite = MasterManager.Instance.ResourceManager.GetSpriteByCharge(charge);
                });
            }

            card.RegisterOnCardEquippedCallback(OnCardEquipped);
            OnCardEquipped(card);
        }

        public void TearDown()
        {
            Card.UnregisterOnCardEquippedCallback(OnCardEquipped);
        }

        public void OnCardEquipped(Card card)
        {   
            EquipToggle.isOn = card.IsEquipped;
        }

        public void OnCardSelected()
        {
            MasterManager.Instance.GameManager.PrizePool.RemoveCard(Card);
            MasterManager.Instance.GameManager.ActivePlayer.AddCard(Card);
        }
    }
}
