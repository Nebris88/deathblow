using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public class GameManager : MonoBehaviour
    {
        public Deck Deck { get; set; }
        public List<Player> Players { get; set; }
        public List<Monster> Monsters { get; set; }
        public PrizePool PrizePool { get; set; }

        private GameRulesManager gameRulesManager;

        private Player _ActivePlayer;
        public Player ActivePlayer
        {
            get => _ActivePlayer; 
            set 
            {
                bool change = (_ActivePlayer != value);
                _ActivePlayer = value;
                if (change && OnActivePlayerChangedCallback != null)
                {
                    OnActivePlayerChangedCallback(ActivePlayer);
                }
            } 
        }

        private Monster _ActiveMonster;
        public Monster ActiveMonster
        {
            get => _ActiveMonster; 
            set 
            {
                bool change = (_ActiveMonster != value);
                _ActiveMonster = value;
                if (change && OnActiveMonsterChangedCallback != null)
                {
                    OnActiveMonsterChangedCallback(ActiveMonster);
                }
            } 
        }

        private Action<Player> OnActivePlayerChangedCallback;
        private Action<Monster> OnActiveMonsterChangedCallback;

        public void Init(GameRulesManager gameRulesManager)
        {
            this.gameRulesManager = gameRulesManager;

            //Load Decks
            Deck = MasterManager.Instance.ResourceManager.LoadDeck();
            //Deck.PrintDeck();

            //Create Players
            Players = new List<Player>();
            for (int x = 1; x < gameRulesManager.playerCount + 1; x++)
            {
                Players.Add(new Player("Player " + x));
            }

            //Create Monsters
            Monsters = new List<Monster>();
            for (int x = 1; x < 5 + 1; x++)
            {
                Monsters.Add(new Monster("Monster " + x));
            }

            PrizePool = new PrizePool();
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

            Monsters.ForEach(monster => {
                monster.RemoveDice();
                monster.ClearCharges();
            });

            Deck.ReturnAndShuffle();

            // Give Initial Dice to Players
            Players.ForEach(player => {
                for (int y = 1; y < gameRulesManager.startingDice + 1; y++)
                {
                    player.AddDie(new Die(player, DieType.Standard));
                }
            });

            // Give Initial Dice to Monsters
            Monsters.ForEach(monster => {
                for (int y = 1; y < 11 + 1; y++)
                {
                    monster.AddDie(new Die(monster, DieType.Monster));
                }
            });

            // Give Initial Cards to Players
            Deck.Deal(PlayersAsCardOwners(), gameRulesManager.startingCards);
        }

        public void FillPrizePool()
        {
            Deck.Draw(gameRulesManager.prizes).ForEach(card => {
                PrizePool.AddCard(card);
            });
        }

        public void EmptyPrizePool()
        {
            new List<Card>(PrizePool.Cards).ForEach(card => {
                card.DiscardToDeck();
            });
        }

        public void TEST()
        {
            // Give Random Charges to Players (for testing)
            Players.ForEach(player => {
                player.RollDice();
                player.AddCharge((Charge)(int)UnityEngine.Random.Range(1, 4));
                player.AddCharge((Charge)(int)UnityEngine.Random.Range(1, 4));
                player.AddCharge((Charge)(int)UnityEngine.Random.Range(1, 4));
            });
        }

        public void RegisterOnActivePlayerChangedCallback(Action<Player> callback)
        {
            OnActivePlayerChangedCallback += callback;
        }

        public void UnregisterOnActivePlayerChangedCallback(Action<Player> callback)
        {
            OnActivePlayerChangedCallback -= callback;
        }

        public void RegisterOnActiveMonsterChangedCallback(Action<Monster> callback)
        {
            OnActiveMonsterChangedCallback += callback;
        }

        public void UnregisterOnActiveMonsterChangedCallback(Action<Monster> callback)
        {
            OnActiveMonsterChangedCallback -= callback;
        }
    }
}
