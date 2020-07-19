using System.Collections;
using System.Collections.Generic;
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

        public Player Player {get; set; }
        public Dictionary<Die, GameObject> Dice;

        public void Init(Player player)
        {
            if ( Utils.isMissing("DicePanelController", new Object[]{ DiePrefab, DiceContainer, RollDiceButton, UnfreezeDiceButton, UnlockDiceButton, AddDieButton }) ) return;

            Dice = new Dictionary<Die, GameObject>();

            Player = player;
            player.RegisterOnDieAddedCallback(OnDieAdded);
            player.RegisterOnDieRemovedCallback(OnDieRemoved);

            RollDiceButton.onClick.AddListener(delegate {  player.RollDice(); });
            UnfreezeDiceButton.onClick.AddListener(delegate { player.UnfreezeDice(); });
            UnlockDiceButton.onClick.AddListener(delegate { player.UnlockDice(); });
            AddDieButton.onClick.AddListener(delegate { player.AddDie(new Die(player, DieType.Standard)); });
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
    }
}