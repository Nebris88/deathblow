﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public class MasterManager : MonoBehaviour
    {
        public static MasterManager Instance;

        public ResourceManager ResourceManager { get; set; }
        public GameManager GameManager { get; set; }
        public OverviewManager OverviewManager { get; set; }
        public ActionManager ActionManager { get; set; }
        public DeckManager DeckManager { get; set; }

        void Awake()
        {
            if (MasterManager.Instance == null)
            {
                MasterManager.Instance = this;
            }

            ResourceManager = gameObject.GetComponent<ResourceManager>();
            GameManager = gameObject.GetComponent<GameManager>();
            //OverviewManager = gameObject.GetComponent<OverviewManager>();
            ActionManager = gameObject.GetComponent<ActionManager>();
            DeckManager = gameObject.GetComponent<DeckManager>();

            if ( Utils.isMissing("MasterManager", new Object[]{ ResourceManager, GameManager, ActionManager, DeckManager }) ) return;

            ResourceManager.Init();
            GameManager.Init();
            // OverviewManager.Init(GameManager.Players);
            ActionManager.Init(GameManager);
            DeckManager.Init(GameManager);
        }

        void Start()
        {
            GameManager.StartNewGame();
            GameManager.TEST();
        }
    }
}
