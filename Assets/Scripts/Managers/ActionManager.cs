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
        public GameObject ActionPanelSelector;

        public Dictionary<Player, GameObject> PlayerActionPanels { get; set; }
        public Dropdown ActionPanelDropDown { get; set; }

        public void Init(List<Player> players)
        {
            if (ActionPanelPrefab == null)
            {
                Debug.LogError("Missing ActionPanelPrefab");
                return;
            }

            if (ActionPanelContainer == null)
            {
                Debug.LogError("Missing ActionPanelContainer");
                return;
            }

            if (ActionPanelSelector == null)
            {
                Debug.LogError("Missing ActionPanelSelector");
                return;
            }

            PlayerActionPanels = new Dictionary<Player, GameObject>();
            ActionPanelDropDown = ActionPanelSelector.GetComponent<Dropdown>();
            List<string> options = new List<string>();

            players.ForEach(player => {
                options.Add(player.Name);

                GameObject playerObject = GameObject.Instantiate(ActionPanelPrefab);
                playerObject.transform.SetParent(ActionPanelContainer.transform);
                playerObject.name = player.Name + " Action Panel";
                
                playerObject.GetComponentInChildren<DicePanelController>().Init(player);
                playerObject.GetComponentInChildren<CardsPanelController>().Init(player);

                PlayerActionPanels.Add(player, playerObject);
            });

            ToggleActiveActionPanel(players[0]);

            ActionPanelDropDown.ClearOptions();
            ActionPanelDropDown.AddOptions(options);
            
            ActionPanelDropDown.onValueChanged.AddListener(delegate { ActionPanelSelection(); });

        }

        public void ActionPanelSelection()
        {
            ToggleActiveActionPanel(PlayerActionPanels.Keys.ToList()[ActionPanelDropDown.value]);
        }

        public void ToggleActiveActionPanel(Player player)
        {
            PlayerActionPanels.Values.ToList().ForEach(panel => {
                panel.SetActive(PlayerActionPanels[player] == panel);
            });
        }
    }
}
