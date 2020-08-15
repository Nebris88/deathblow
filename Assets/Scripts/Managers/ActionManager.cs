using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class ActionManager : MonoBehaviour
    {
        public GameObject ActionPanelPrefab;
        public GameObject ActionPanelContainer;
        public Dropdown ActionPanelDropDown;

        public GameManager GameManager { get; set; }
        public Dictionary<Player, GameObject> PlayerActionPanels { get; set; }
        public bool initialized = false;

        public void Init(GameManager gameManager)
        {
            GameManager = gameManager;
            List<Player> players = gameManager.Players;
            gameManager.RegisterOnActivePlayerChangedCallback(ToggleActiveActionPanel);

            if ( Utils.isMissing("ActionManager", new Object[]{ ActionPanelPrefab, ActionPanelContainer, ActionPanelDropDown }) ) return;

            PlayerActionPanels = new Dictionary<Player, GameObject>();
            List<string> options = new List<string>();

            players.ForEach(player => {
                options.Add(player.Name);

                GameObject playerActionPanelObject = GameObject.Instantiate(ActionPanelPrefab);
                playerActionPanelObject.transform.SetParent(ActionPanelContainer.transform);
                playerActionPanelObject.name = player.Name + " Action Panel";
                
                playerActionPanelObject.GetComponentInChildren<DicePanelController>().Init(player);
                playerActionPanelObject.GetComponentInChildren<CardsPanelController>().Init(player);
                playerActionPanelObject.GetComponentInChildren<StatsPanelController>().Init(player);

                PlayerActionPanels.Add(player, playerActionPanelObject);
            });

            ActionPanelDropDown.ClearOptions();
            ActionPanelDropDown.AddOptions(options);
            ActionPanelDropDown.onValueChanged.AddListener(delegate { gameManager.ActivePlayer = PlayerActionPanels.Keys.ToList()[ActionPanelDropDown.value]; });
            gameManager.ActivePlayer = PlayerActionPanels.Keys.ToList()[ActionPanelDropDown.value];

            initialized = true;
        }

        public void ToggleActiveActionPanel(Player player)
        {
            PlayerActionPanels.Values.ToList().ForEach(panel => {
                panel.SetActive(PlayerActionPanels[player] == panel);
            });
        }

        void Update()
        {
            if (initialized)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ActionPanelDropDown.value = ActionPanelDropDown.value - 1 < 0 ? ActionPanelDropDown.options.Count - 1 : ActionPanelDropDown.value - 1;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Tab))
                { 
                    ActionPanelDropDown.value = ActionPanelDropDown.value + 1 >= ActionPanelDropDown.options.Count ? 0 : ActionPanelDropDown.value + 1;
                }
            }
        }
    }
}
