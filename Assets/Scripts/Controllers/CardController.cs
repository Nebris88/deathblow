using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class CardController : MonoBehaviour
    {
        public GameObject CardNameGameObject;
        public GameObject EquipToggleGameObject; 
        public GameObject DiscardButtonGameObject; 
        public GameObject CardInfoPanel; 
        public GameObject CardInfoPrefab;

        public Text CardName { get; set; }
        public Toggle EquipToggle { get; set; }
        public Button DiscardButton { get; set; }

        public Card Card { get; set; }

        public void Init(Card card)
        {
            if (CardNameGameObject == null)
            {
                Debug.LogError("Missing CardNameGameObject");
                return;
            }
            if (EquipToggleGameObject == null)
            {
                Debug.LogError("Missing EquipToggleGameObject");
                return;
            }
            if (DiscardButtonGameObject == null)
            {
                Debug.LogError("Missing DiscardButtonGameObject");
                return;
            }
            if (CardInfoPanel == null)
            {
                Debug.LogError("Missing CardInfoPanel");
                return;
            }
            if (CardInfoPrefab == null)
            {
                Debug.LogError("Missing CardInfoPrefab");
                return;
            }

            Card = card;

            CardName = CardNameGameObject.GetComponent<Text>();
            EquipToggle = EquipToggleGameObject.GetComponent<Toggle>();
            DiscardButton = DiscardButtonGameObject.GetComponent<Button>();

            CardName.text = card.Name;
            EquipToggle.onValueChanged.AddListener(delegate { card.IsEquipped = EquipToggle.isOn; });
            DiscardButton.onClick.AddListener(delegate { card.DiscardToDeck(); });
            EquipToggleGameObject.SetActive(card.IsEquippable());

            if (card.IsEquipment)
            {   
                card.Bonuses.ForEach(dieFace => {
                    GameObject cardInfoObject = GameObject.Instantiate(CardInfoPrefab);
                    cardInfoObject.transform.SetParent(CardInfoPanel.transform);
                    cardInfoObject.name = card.Name;
                    cardInfoObject.GetComponent<Image>().sprite = MasterManager.Instance.ResourceManager.GetSpriteByDieFace(dieFace);
                });
            }

            if (card.IsSpell)
            {   
                card.Costs.ForEach(charge => {
                    GameObject cardInfoObject = GameObject.Instantiate(CardInfoPrefab);
                    cardInfoObject.transform.SetParent(CardInfoPanel.transform);
                    cardInfoObject.name = card.Name;
                    cardInfoObject.GetComponent<Image>().sprite = MasterManager.Instance.ResourceManager.GetSpriteByCharge(charge);
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
    }
}
