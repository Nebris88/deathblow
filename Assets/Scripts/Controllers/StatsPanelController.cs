using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class StatsPanelController : MonoBehaviour
    {
        public GameObject StatPanelPrefab;

        public Player Player {get; set; }

        public void Init(Player player)
        {
            if ( Utils.isMissing("StatsPanelController", new UnityEngine.Object[]{ StatPanelPrefab }) ) return;

            Player = player;

            foreach (DieFace dieFace in Dice.GetDieFaces(DieType.Standard))
            {
                GameObject statPanelObject = GameObject.Instantiate(StatPanelPrefab);
                statPanelObject.transform.SetParent(transform);
                statPanelObject.name = player.Name + " " + dieFace + " Stat Panel";
                statPanelObject.GetComponentInChildren<StatPanelController>().Init(player, dieFace);
            }
        }
    }
}