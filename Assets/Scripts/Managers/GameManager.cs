using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public class GameManager : MonoBehaviour
    {
        //Initial Conditions
        public int startingDice = 5;
        public int startingCards = 5;

        public Deck Deck { get; set; }
        public List<Player> Players { get; set; }

        public void Init()
        {
            //Load Decks
            Deck = new Deck();
            //Deck.PrintDeck();

            //Create Players
            Players = new List<Player>();
            for (int x = 1; x < 5; x++)
            {
                string playerName = "Player " + x;
                Player player = new Player(playerName);
                Players.Add(player);
            }
        }

        private List<CardOwner> PlayersAsCardOwners()
        {
            List<CardOwner> cardOwners = new List<CardOwner>();
            Players.ForEach(player => {
                cardOwners.Add(player);
            });
            return cardOwners;
        }

        public void StartNewGame()
        {
            Players.ForEach(player => {
                player.RemoveDice();
                player.ClearCharges();
            });

            Deck.ReturnAndShuffle();

            // Give Initial Dice to Players
            Players.ForEach(player => {
                for (int y = 1; y < startingDice + 1; y++)
                {
                    player.AddDie(new Die(player, DieType.Standard));
                }
            });

            // Give Initial Cards to Players
            Deck.Deal(PlayersAsCardOwners(), startingCards);
        }

        public void TEST()
        {
            // Give Random Charges to Players (for testing)
            Players.ForEach(player => {
                player.RollDice();
                player.AddCharge((Charge)(int)Random.Range(1, 4));
                player.AddCharge((Charge)(int)Random.Range(1, 4));
                player.AddCharge((Charge)(int)Random.Range(1, 4));
            });
        }
    }
}
