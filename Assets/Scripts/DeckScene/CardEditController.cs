using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class CardEditController : MonoBehaviour
    {
        public InputField NameField;
        public InputField EffectField;
        public Dropdown TypeDropdown;
        public Dropdown EquipmentTypeDropdown;
        public Dropdown Bonus1Dropdown;
        public Dropdown Bonus2Dropdown;
        public Dropdown Bonus3Dropdown;
        public Dropdown Cost1Dropdown;
        public Dropdown Cost2Dropdown;
        public Dropdown Cost3Dropdown;
        public Button DeleteButton;

        public GameObject BonusPanel;
        public GameObject CostPanel;

        public Card Card { get; set; }

        public void Init(Card card)
        {
            if ( Utils.isMissing("CardEditController", new UnityEngine.Object[]{ NameField, EffectField, TypeDropdown, EquipmentTypeDropdown, Bonus1Dropdown, 
                Bonus2Dropdown, Bonus3Dropdown, Cost1Dropdown, Cost2Dropdown, Cost3Dropdown, DeleteButton, BonusPanel, CostPanel }) ) return;

            Card = card;

            //set dropdown options
            List<string> typeOptions = new List<string>();
            List<string> equipmentTypeOptions = new List<string>();
            List<string> bonusOptions = new List<string>();
            List<string> costOptions = new List<string>();

            bonusOptions.Add("none");
            costOptions.Add("none");

            foreach (CardType cardType in Enum.GetValues(typeof(CardType)))
            {
                typeOptions.Add(cardType.ToString());
            }

            foreach (EquipmentType equipmentType in Enum.GetValues(typeof(EquipmentType)))
            {
                equipmentTypeOptions.Add(equipmentType.ToString());
            }

            foreach (CardBonus bonus in Enum.GetValues(typeof(CardBonus)))
            {
                bonusOptions.Add(bonus.ToString());
            }

            foreach (Charge charge in Enum.GetValues(typeof(Charge)))
            {
                if (!charge.ToString().Contains("onster")) costOptions.Add(charge.ToString());
            }

            TypeDropdown.ClearOptions();
            EquipmentTypeDropdown.ClearOptions();
            Bonus1Dropdown.ClearOptions();
            Bonus2Dropdown.ClearOptions();
            Bonus3Dropdown.ClearOptions();
            Cost1Dropdown.ClearOptions();
            Cost2Dropdown.ClearOptions();
            Cost3Dropdown.ClearOptions();

            TypeDropdown.AddOptions(typeOptions);
            EquipmentTypeDropdown.AddOptions(equipmentTypeOptions);
            Bonus1Dropdown.AddOptions(bonusOptions);
            Bonus2Dropdown.AddOptions(bonusOptions);
            Bonus3Dropdown.AddOptions(bonusOptions);
            Cost1Dropdown.AddOptions(costOptions);
            Cost2Dropdown.AddOptions(costOptions);
            Cost3Dropdown.AddOptions(costOptions);

            //set fields to current values.
            NameField.text = Card.Name;
            EffectField.text = Card.Effect;

            TypeDropdown.value = (int) Card.CardType;
            EquipmentTypeDropdown.value = (int) Card.EquipmentType;

            Bonus1Dropdown.value = Card.Bonuses.Count > 0 ? 1 + (int) Card.Bonuses[0] : 0;
            Bonus2Dropdown.value = Card.Bonuses.Count > 1 ? 1 + (int) Card.Bonuses[1] : 0;
            Bonus3Dropdown.value = Card.Bonuses.Count > 2 ? 1 + (int) Card.Bonuses[2] : 0;
            
            Cost1Dropdown.value = Card.Costs.Count > 0 ? 1 + (int) Card.Costs[0] : 0;
            Cost2Dropdown.value = Card.Costs.Count > 1 ? 1 + (int) Card.Costs[1] : 0;
            Cost3Dropdown.value = Card.Costs.Count > 2 ? 1 + (int) Card.Costs[2] : 0;

            //set listener delegates
            NameField.onValueChanged.AddListener(delegate { Card.Name = NameField.text; });
            EffectField.onValueChanged.AddListener(delegate { Card.Effect = EffectField.text; });

            TypeDropdown.onValueChanged.AddListener(delegate { onTypeChanged(); });
            EquipmentTypeDropdown.onValueChanged.AddListener(delegate { onEquipmentTypeChanged(); });
            Bonus1Dropdown.onValueChanged.AddListener(delegate { onBonusesChanged(); });
            Bonus2Dropdown.onValueChanged.AddListener(delegate { onBonusesChanged(); });
            Bonus3Dropdown.onValueChanged.AddListener(delegate { onBonusesChanged(); });
            Cost1Dropdown.onValueChanged.AddListener(delegate { onCostsChanged(); });
            Cost2Dropdown.onValueChanged.AddListener(delegate { onCostsChanged(); });
            Cost3Dropdown.onValueChanged.AddListener(delegate { onCostsChanged(); });

            DeleteButton.onClick.AddListener(delegate { onDelete(); });

            onTypeChanged();
            onEquipmentTypeChanged();
            onBonusesChanged();
            onCostsChanged();
        }

        private void onTypeChanged()
        {
            Card.CardType = (CardType) TypeDropdown.value;
            BonusPanel.SetActive(Card.CardType == CardType.Equipment);
            CostPanel.SetActive(Card.CardType == CardType.Spell);
        }

        private void onEquipmentTypeChanged()
        {
            Card.EquipmentType = (EquipmentType) EquipmentTypeDropdown.value;
        }

        private void onBonusesChanged()
        {
            List<CardBonus> bonuses = new List<CardBonus>();
            if (Bonus1Dropdown.value != 0) bonuses.Add((CardBonus)(Bonus1Dropdown.value - 1));
            if (Bonus2Dropdown.value != 0) bonuses.Add((CardBonus)(Bonus2Dropdown.value - 1));
            if (Bonus3Dropdown.value != 0) bonuses.Add((CardBonus)(Bonus3Dropdown.value - 1));
            Card.Bonuses = bonuses;
        }

        private void onCostsChanged()
        {
            List<Charge> costs = new List<Charge>();
            if (Cost1Dropdown.value != 0) costs.Add((Charge)(Cost1Dropdown.value - 1));
            if (Cost2Dropdown.value != 0) costs.Add((Charge)(Cost2Dropdown.value - 1));
            if (Cost3Dropdown.value != 0) costs.Add((Charge)(Cost3Dropdown.value - 1));
            Card.Costs = costs;
        }

        private void onDelete()
        {
            Card.Deck.RemoveCard(Card);
        }
    }
}