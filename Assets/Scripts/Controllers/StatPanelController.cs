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

            IncreaseButton.onClick.AddListener(delegate { IncreaseBaseStat(true); });
            DecreaseButton.onClick.AddListener(delegate { IncreaseBaseStat(false); });

            player.RegisterOnCardAddedCallback(OnCardAdded);
            player.RegisterOnCardRemovedCallback(OnCardRemoved);
            player.RegisterOnDieAddedCallback(OnDieAdded);
            player.RegisterOnDieRemovedCallback(OnDieRemoved);
            player.RegisterOnChargeChangedCallback(OnChargeChanged);
        }

        private int GetBaseStat()
        {
            switch (DieFace)
            {
                case DieFace.Power:
                    return Player.GetCharges(Charge.Power_Energy);
                case DieFace.Mind:
                    return Player.GetCharges(Charge.Mind_Energy);
                case DieFace.Life:
                    return Player.GetCharges(Charge.Life_Energy);
                default:
                    return baseStat;
            }
        }

        public void OnStatChange()
        {
            //Base
            BaseText.text = GetBaseStat().ToString();

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
            TotalText.text = (baseStat + Math.Floor( (diceFaces + cardFaces) / MasterManager.Instance.GameRulesManager.match )).ToString();
        }

        //BASE
        public void IncreaseBaseStat(bool increase)
        {
            switch (DieFace)
            {
                case DieFace.Power:
                    if (increase) Player.AddCharge(Charge.Power_Energy);
                    else Player.RemoveCharge(Charge.Power_Energy);
                    //increase ? Player.AddCharge(Charge.Power_Energy) : Player.RemoveCharge(Charge.Power_Energy);
                    break;
                case DieFace.Mind:
                    if (increase) Player.AddCharge(Charge.Mind_Energy);
                    else Player.RemoveCharge(Charge.Mind_Energy);
                    //increase ? Player.AddCharge(Charge.Mind_Energy) : Player.RemoveCharge(Charge.Mind_Energy);
                    break;
                case DieFace.Life:
                    if (increase) Player.AddCharge(Charge.Life_Energy);
                    else Player.RemoveCharge(Charge.Life_Energy);
                    //increase ? Player.AddCharge(Charge.Life_Energy) : Player.RemoveCharge(Charge.Life_Energy);
                    break;
                default:
                    baseStat += increase ? 1 : -1;
                    break;
            }
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

        //Charge
        public void OnChargeChanged(Charge charge, ChargeOwner owner)
        {
            OnStatChange();
        }
    }
}