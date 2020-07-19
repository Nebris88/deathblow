using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class StatPanelController : MonoBehaviour
    {
        public float match = 2f;

        public Text LabelText;
        public Button IncreaseButton;
        public Button DecreaseButton;
        public Text BaseText;
        public Text DiceText;
        public Text CardsText;
        public Text TotalText;

        public Player Player {get; set; }
        public DieFace DieFace {get; set; }
        private int baseStat = 0;

        public void Init(Player player, DieFace dieFace)
        {
            if ( Utils.isMissing("StatPanelController", new UnityEngine.Object[]{ LabelText, IncreaseButton, DecreaseButton, BaseText, DiceText, CardsText, TotalText }) ) return;

            Player = player;
            DieFace = dieFace;

            if (dieFace == DieFace.Attack) baseStat = 1;
            
            LabelText.text = dieFace + " ";

            IncreaseButton.onClick.AddListener(delegate { IncreaseBaseState(true); });
            DecreaseButton.onClick.AddListener(delegate { IncreaseBaseState(false); });

            player.RegisterOnCardAddedCallback(OnCardAdded);
            player.RegisterOnCardRemovedCallback(OnCardRemoved);
            player.RegisterOnDieAddedCallback(OnDieAdded);
            player.RegisterOnDieRemovedCallback(OnDieRemoved);
        }

        public void OnStatChange()
        {
            //Base
            BaseText.text = baseStat.ToString();

            //Dice
            int diceFaces = 0;
            Player.Dice.ForEach(die => {
                if (die.DieFace == DieFace) diceFaces++;
            });
            DiceText.text = diceFaces.ToString();

            //Cards
            int cardFaces = 0;
            Player.Cards.ForEach(card => {
                if (card.IsEquipped)
                {
                    card.Bonuses.ForEach(bonus => {
                        if (bonus == DieFace) cardFaces++;
                    });
                }
            });
            CardsText.text = cardFaces.ToString();

            //Total
            TotalText.text = (baseStat + Math.Floor( (diceFaces + cardFaces) / match )).ToString();
        }

        //BASE
        public void IncreaseBaseState(bool increase)
        {
            baseStat += increase ? 1 : -1;
            OnStatChange();
        }

        //CARDS
        public void OnCardAdded(Card card)
        {
            card.RegisterOnCardEquippedCallback(OnCardEquipped);
        }

        public void OnCardRemoved(Card card)
        {
            card.UnregisterOnCardEquippedCallback(OnCardEquipped);
        }

        public void OnCardEquipped(Card card)
        {
            OnStatChange();
        }

        //DICE
        public void OnDieAdded(Die die)
        {
            die.RegisterOnDieChangedCallback(OnDieChanged);
        }

        public void OnDieRemoved(Die die)
        {
            die.UnregisterOnDieChangedCallback(OnDieChanged);
        }

        public void OnDieChanged(Die die)
        {
            OnStatChange();
        }
    }
}