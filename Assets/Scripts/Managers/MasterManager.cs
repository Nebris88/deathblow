using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public class MasterManager : MonoBehaviour
    {
        public static MasterManager Instance;

        public ResourceManager ResourceManager { get; set; }
        public GameRulesManager GameRulesManager { get; set; }
        public GameManager GameManager { get; set; }
        public MenuManager MenuManager { get; set; }
        public OverviewManager OverviewManager { get; set; }
        public ActionManager ActionManager { get; set; }
        public MonsterManager MonsterManager { get; set; }
        public DeckManager DeckManager { get; set; }
        public PrizeManager PrizeManager { get; set; }

        void Awake()
        {
            if (MasterManager.Instance == null)
            {
                MasterManager.Instance = this;
            }

            ResourceManager = gameObject.GetComponent<ResourceManager>();
            GameRulesManager = gameObject.GetComponent<GameRulesManager>();
            GameManager = gameObject.GetComponent<GameManager>();
            MenuManager = gameObject.GetComponent<MenuManager>();
            OverviewManager = gameObject.GetComponent<OverviewManager>();
            ActionManager = gameObject.GetComponent<ActionManager>();
            MonsterManager = gameObject.GetComponent<MonsterManager>();
            DeckManager = gameObject.GetComponent<DeckManager>();
            PrizeManager = gameObject.GetComponent<PrizeManager>();

            if ( Utils.isMissing("MasterManager", new Object[]{ ResourceManager, GameRulesManager, GameManager, MenuManager, OverviewManager, ActionManager, 
                MonsterManager, DeckManager, PrizeManager }) ) return;

            ResourceManager.Init();
            GameRulesManager.Init();
            GameManager.Init(GameRulesManager);
            MenuManager.Init(GameManager);
            OverviewManager.Init(GameManager);
            ActionManager.Init(GameManager);
            MonsterManager.Init(GameManager);
            DeckManager.Init(GameManager);
            PrizeManager.Init(GameManager);
        }

        void Start()
        {
            GameManager.StartNewGame();
            //ResourceManager.SaveDeck(GameManager.Deck);
            //ResourceManager.LoadDeck(GameManager.Deck);
            //GameManager.TEST();
        }
    }
}
