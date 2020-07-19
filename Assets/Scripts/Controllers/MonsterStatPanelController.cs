using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class MonsterStatPanelController : MonoBehaviour
    {
        public Text LabelText;
        public Button IncreaseButton;
        public Button DecreaseButton;
        public Text BaseText;
        public Text DiceText;
        public Text TotalText;

        public Monster Monster {get; set; }
        public DieFace DieFace {get; set; }
        private int baseStat = 0;

        public void Init(Monster monster, DieFace dieFace)
        {
            if ( Utils.isMissing("StatPanelController", new UnityEngine.Object[]{ LabelText, IncreaseButton, DecreaseButton, BaseText, DiceText, TotalText }) ) return;

            Monster = monster;
            DieFace = dieFace;

            if (dieFace == DieFace.Attack) baseStat = 1;
            LabelText.text = dieFace + " ";

            IncreaseButton.onClick.AddListener(delegate { IncreaseBaseStat(true); });
            DecreaseButton.onClick.AddListener(delegate { IncreaseBaseStat(false); });

            monster.RegisterOnDieAddedCallback(OnDieAdded);
            monster.RegisterOnDieRemovedCallback(OnDieRemoved);
            monster.RegisterOnChargeChangedCallback(OnChargeChanged);
        }

        private int GetBaseStat()
        {
            switch (DieFace)
            {
                case DieFace.Monster_1:
                    return Monster.GetCharges(Charge.Monster_1);
                case DieFace.Monster_2:
                    return Monster.GetCharges(Charge.Monster_2);
                case DieFace.Monster_3:
                    return Monster.GetCharges(Charge.Monster_3);
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
            Monster.Dice.ForEach(die => {
                if (die.DieFace == DieFace) diceFaces++;
            });
            DiceText.text = diceFaces.ToString();

            //Total
            TotalText.text = (baseStat + Math.Floor( diceFaces / MasterManager.Instance.GameRulesManager.match )).ToString();
        }

        //BASE
        public void IncreaseBaseStat(bool increase)
        {
            switch (DieFace)
            {
                case DieFace.Monster_1:
                    if (increase) Monster.AddCharge(Charge.Monster_1);
                    else Monster.RemoveCharge(Charge.Monster_1);
                    break;
                case DieFace.Monster_2:
                    if (increase) Monster.AddCharge(Charge.Monster_2);
                    else Monster.RemoveCharge(Charge.Monster_2);
                    break;
                case DieFace.Monster_3:
                    if (increase) Monster.AddCharge(Charge.Monster_3);
                    else Monster.RemoveCharge(Charge.Monster_3);
                    break;
                default:
                    baseStat += increase ? 1 : -1;
                    break;
            }
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