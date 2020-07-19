using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class DicePanelController : MonoBehaviour
    {
        public GameObject DiePrefab;
        public GameObject DiceContainer;
        public Button RollDiceButton;
        public Button UnfreezeDiceButton;
        public Button UnlockDiceButton;
        public Button AddDieButton;
        public Button AddChargesButton;

        public DieOwner DieOwner {get; set; }
        public Dictionary<Die, GameObject> Dice;

        public void Init(DieOwner dieOwner)
        {
            if ( Utils.isMissing("DicePanelController", new UnityEngine.Object[]{ DiePrefab, DiceContainer, RollDiceButton, UnfreezeDiceButton, UnlockDiceButton, AddDieButton, 
                AddChargesButton }) ) return;

            Dice = new Dictionary<Die, GameObject>();

            DieOwner = dieOwner;
            dieOwner.RegisterOnDieAddedCallback(OnDieAdded);
            dieOwner.RegisterOnDieRemovedCallback(OnDieRemoved);

            RollDiceButton.onClick.AddListener(delegate {  dieOwner.RollDice(); });
            UnfreezeDiceButton.onClick.AddListener(delegate { dieOwner.UnfreezeDice(); });
            UnlockDiceButton.onClick.AddListener(delegate { dieOwner.UnlockDice(); });
            AddDieButton.onClick.AddListener(delegate { dieOwner.AddDie(new Die(dieOwner, DieType.Standard)); });
            AddChargesButton.onClick.AddListener(delegate { AddCharges(); });
        }

        public void OnDieAdded(Die die)
        {
            GameObject dieObject = GameObject.Instantiate(DiePrefab);
            dieObject.transform.SetParent(DiceContainer.transform);
            dieObject.name = "Die";
            dieObject.GetComponent<DieController>().Init(die);
            Dice.Add(die, dieObject);
        }
        
        public void OnDieRemoved(Die die)
        {
            if (!Dice.ContainsKey(die))
            {
                Debug.LogError("Attempting to remove die from die container lacking said die.");
                return;
            }
            GameObject dieObject = Dice[die];
            dieObject.GetComponent<DieController>().TearDown();
            Dice.Remove(die);
            GameObject.Destroy(dieObject);
        }

        public void AddCharges()
        {
            if (!(DieOwner is Entity)) return;
            Entity entity = (Entity) DieOwner;

            foreach (DieFace dieFace in Enum.GetValues(typeof(DieFace)))
            {
                //Dice
                int diceFaces = 0;
                    entity.Dice.ForEach(die => {
                    if (die.DieFace == dieFace) diceFaces++;
                });

                //Cards
                int cardFaces = 0;
                if (entity is CardOwner)
                {
                    ((CardOwner) entity).Cards.ForEach(card => {
                        if (card.IsEquipped)
                        {
                            card.Bonuses.ForEach(bonus => {
                                if (bonus == dieFace) cardFaces++;
                            });
                        }
                    });
                }

                //Total
                int total = (int)Math.Floor((diceFaces + cardFaces) / MasterManager.Instance.GameRulesManager.match);
                if (total == 0) continue;
                
                //Add to respective charge
                switch(dieFace)
                {
                    case DieFace.Power:
                        entity.AddCharge(Charge.Power_Energy, total);
                        break;
                    case DieFace.Mind:
                        entity.AddCharge(Charge.Mind_Energy, total);
                        break;
                    case DieFace.Life:
                        entity.AddCharge(Charge.Life_Energy, total);
                        break;
                    case DieFace.Monster_1:
                        entity.AddCharge(Charge.Monster_1, total);
                        break;
                    case DieFace.Monster_2:
                        entity.AddCharge(Charge.Monster_2, total);
                        break;
                    case DieFace.Monster_3:
                        entity.AddCharge(Charge.Monster_3, total);
                        break;
                }
            }
        }
    }
}