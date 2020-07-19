using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class MonsterStatsPanelController : MonoBehaviour
    {
        public GameObject MonsterStatPanelPrefab;

        public Monster Monster {get; set; }

        public void Init(Monster monster)
        {
            if ( Utils.isMissing("StatsPanelController", new UnityEngine.Object[]{ MonsterStatPanelPrefab }) ) return;

            Monster = monster;

            foreach (DieFace dieFace in Dice.GetDieFaces(DieType.Monster).ToList().Distinct())
            {
                GameObject statPanelObject = GameObject.Instantiate(MonsterStatPanelPrefab);
                statPanelObject.transform.SetParent(transform);
                statPanelObject.name = monster.Name + " " + dieFace + " Stat Panel";
                statPanelObject.GetComponentInChildren<MonsterStatPanelController>().Init(monster, dieFace);
            }
        }
    }
}