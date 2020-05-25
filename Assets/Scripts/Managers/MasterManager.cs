using System.Collections;
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

        void Awake()
        {
            if (MasterManager.Instance == null)
            {
                MasterManager.Instance = this;
            }

            ResourceManager = gameObject.GetComponent<ResourceManager>();
            if (ResourceManager == null) 
            {
                Debug.LogError("Missing ResourceManager");
                return;
            }
            ResourceManager.Init();

            GameManager = gameObject.GetComponent<GameManager>();
            if (GameManager == null) 
            {
                Debug.LogError("Missing GameManager");
                return;
            }
            GameManager.Init();

            /*
            OverviewManager = gameObject.GetComponent<OverviewManager>();
            if (OverviewManager == null) 
            {   
                Debug.LogError("Missing OverviewManager");
                return;
            }
            OverviewManager.Init(GameManager.Players);
            */
            
            ActionManager = gameObject.GetComponent<ActionManager>();
            if (ActionManager == null) 
            {   
                Debug.LogError("Missing ActionManager");
                return;
            }
            ActionManager.Init(GameManager.Players);
        }

        void Start()
        {
            GameManager.StartNewGame();
            GameManager.TEST();
        }
    }
}
