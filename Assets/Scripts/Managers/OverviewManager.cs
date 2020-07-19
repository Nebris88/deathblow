using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class OverviewManager : MonoBehaviour
    {
        public GameObject OverviewPrefab;
        public GameObject OverviewPanelContainer;

        public Dictionary<Player, OverviewController> PlayerPanels { get; set; }
        public Dictionary<Monster, OverviewController> MonsterPanels { get; set; }

        public void Init(GameManager gameManager)
        {
            if ( Utils.isMissing("CardController", new Object[]{ OverviewPrefab, OverviewPanelContainer }) ) return;

            MonsterPanels = new Dictionary<Monster, OverviewController>();
            gameManager.Monsters.ForEach(monster => {
                MonsterPanels.Add(monster, CreateOverviewPanel(monster));
            });

            PlayerPanels = new Dictionary<Player, OverviewController>();
            gameManager.Players.ForEach(player => {
                PlayerPanels.Add(player, CreateOverviewPanel(player));
            });

            gameManager.RegisterOnActiveMonsterChangedCallback(OnActiveMonsterChanged);
            gameManager.RegisterOnActivePlayerChangedCallback(OnActivePlayerChanged);
        }

        private OverviewController CreateOverviewPanel(Entity entity)
        {
            GameObject overviewPanelObject = GameObject.Instantiate(OverviewPrefab);
            overviewPanelObject.transform.SetParent(OverviewPanelContainer.transform);
            overviewPanelObject.name = entity.Name;
            OverviewController overviewController = overviewPanelObject.GetComponent<OverviewController>();
            overviewController.Init(entity);

            return overviewController;
        }

        public void OnActiveMonsterChanged(Monster monster)
        {
            MonsterPanels.Keys.ToList().ForEach(key => {
                MonsterPanels[key].gameObject.SetActive(key == monster);
            });
        }

        public void OnActivePlayerChanged(Player player)
        {
            PlayerPanels.Keys.ToList().ForEach(key => {
                PlayerPanels[key].SetActiveIdentifier(key == player);
            });
        }
    }
}
