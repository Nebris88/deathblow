using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class OverviewManager : MonoBehaviour
    {
        public GameObject PlayerInfoPrefab;
        public GameObject OverviewPanel;

        public Dictionary<Player, Text> PlayerPanels { get; set; }

        public void Init(List<Player> players)
        {
            if (PlayerInfoPrefab == null)
            {
                Debug.LogError("Missing PlayerInfoPrefab");
                return;
            }

            if (OverviewPanel == null)
            {
                Debug.LogError("Missing OverviewPanel");
                return;
            }

            PlayerPanels = new Dictionary<Player, Text>();

            players.ForEach(player => {
                GameObject playerObject = GameObject.Instantiate(PlayerInfoPrefab);
                playerObject.transform.SetParent(OverviewPanel.transform);
                //player.RegisterOnHandChangedCallback(OnHandChanged);
                //player.RegisterOnChargeChangedCallback(OnChargeChanged);
                PlayerPanels.Add(player, playerObject.GetComponent<Text>());
            });
        }

        void Update()
        {
            //FIXME
            PlayerPanels.Keys.ToList().ForEach(player => {
                ChangeOverview(player);
            });
        }

        public void OnHandChanged(Card card)
        {
            ChangeOverview((Player)card.CardOwner);
        }

        public void OnChargeChanged(Charge charge, ChargeOwner chargeOwner)
        {
            ChangeOverview((Player)chargeOwner);
        }

        private void ChangeOverview(Player player)
        {
            string overview = player.Name + " - ";

            player.Dice.ForEach(die => {
                overview += die.DieFace.ToString().Substring(0,1) + " ";   
            });
            overview += "- ";

            player.Cards.ForEach(card => {
                overview += card.Name + " ";   
            });
            overview += "- ";

            player.Charges.Keys.ToList().ForEach(charge => {
                for(int x = 0; x < player.Charges[charge]; x++)
                {
                    overview += charge.ToString().Substring(0,1) + " ";
                }
            });

            PlayerPanels[player].text = overview;
        }
    }
}
