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
        public GameObject RollDiceGameObject;
        public GameObject UnfreezeDiceGameObject;
        public GameObject UnlockDiceGameObject;
        public GameObject AddDieGameObject;

        public Player Player {get; set; }
        public Dictionary<Die, GameObject> Dice;

        public void Init(Player player)
        {
            if (DiePrefab == null)
            {
                Debug.LogError("Missing DiePrefab");
                return;
            }

            if (DiceContainer == null)
            {
                Debug.LogError("Missing DiceContainer");
                return;
            }

            if (RollDiceGameObject == null)
            {
                Debug.LogError("Missing RollDiceGameObject");
                return;
            }

            if (UnfreezeDiceGameObject == null)
            {
                Debug.LogError("Missing UnfreezeDiceGameObject");
                return;
            }

            if (UnlockDiceGameObject == null)
            {
                Debug.LogError("Missing UnlockDiceGameObject");
                return;
            }

            if (AddDieGameObject == null)
            {
                Debug.LogError("Missing AddDieGameObject");
                return;
            }

            Dice = new Dictionary<Die, GameObject>();

            Player = player;
            player.RegisterOnDieAddedCallback(OnDieAdded);
            player.RegisterOnDieRemovedCallback(OnDieRemoved);

            RollDiceGameObject.GetComponent<Button>().onClick.AddListener(delegate {  player.RollDice(); });
            UnfreezeDiceGameObject.GetComponent<Button>().onClick.AddListener(delegate { player.UnfreezeDice(); });
            UnlockDiceGameObject.GetComponent<Button>().onClick.AddListener(delegate { player.UnlockDice(); });
            AddDieGameObject.GetComponent<Button>().onClick.AddListener(delegate { player.AddDie(new Die(player, DieType.Standard)); });
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