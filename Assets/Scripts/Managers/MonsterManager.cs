using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class MonsterManager : MonoBehaviour
    {
        public GameObject MonsterPanelPrefab;
        public GameObject MonsterPanelContainer;
        public Dropdown MonsterPanelDropDown;

        public Dictionary<Monster, GameObject> MonsterPanels { get; set; }

        public void Init(GameManager gameManager)
        {
            List<Monster> monsters = gameManager.Monsters;
            gameManager.RegisterOnActiveMonsterChangedCallback(ToggleActiveMonsterPanel);

            if ( Utils.isMissing("MonsterManager", new Object[]{ MonsterPanelPrefab, MonsterPanelContainer, MonsterPanelDropDown }) ) return;

            MonsterPanels = new Dictionary<Monster, GameObject>();
            List<string> options = new List<string>();

            monsters.ForEach(monster => {
                options.Add(monster.Name);

                GameObject monsterPanelObject = GameObject.Instantiate(MonsterPanelPrefab);
                monsterPanelObject.transform.SetParent(MonsterPanelContainer.transform);
                monsterPanelObject.name = monster.Name + " Monster Panel";
                
                monsterPanelObject.GetComponentInChildren<DicePanelController>().Init(monster);
                monsterPanelObject.GetComponentInChildren<MonsterStatsPanelController>().Init(monster);
                monsterPanelObject.GetComponentInChildren<AbilitiesController>().Init(monster);

                MonsterPanels.Add(monster, monsterPanelObject);
            });

            MonsterPanelDropDown.ClearOptions();
            MonsterPanelDropDown.AddOptions(options);
            MonsterPanelDropDown.onValueChanged.AddListener(delegate { gameManager.ActiveMonster = MonsterPanels.Keys.ToList()[MonsterPanelDropDown.value]; });
            gameManager.ActiveMonster = MonsterPanels.Keys.ToList()[MonsterPanelDropDown.value];
        }
        
        public void ToggleActiveMonsterPanel(Monster monster)
        {
            MonsterPanels.Values.ToList().ForEach(panel => {
                panel.SetActive(MonsterPanels[monster] == panel);
            });
        }
        
    }
}
