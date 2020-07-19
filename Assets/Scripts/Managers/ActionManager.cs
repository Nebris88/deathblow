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
            if ( Utils.isMissing("ActionManager", new Object[]{ ActionPanelPrefab, ActionPanelContainer, ActionPanelSelector }) ) return;

            PlayerActionPanels = new Dictionary<Player, GameObject>();
            ActionPanelDropDown = ActionPanelSelector.GetComponent<Dropdown>();
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
