using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public class DeckSceneManager : MonoBehaviour
    {
        public static DeckSceneManager Instance;

        public ResourceManager ResourceManager { get; set; }
        public DeckEditController DeckEditController;
        public DeckMenuController DeckMenuController;

        private Deck _Deck;
        public Deck Deck 
        {
            get => _Deck; 
            set 
            {
                _Deck = value;
                if (OnDeckLoadedCallback != null)
                {
                    OnDeckLoadedCallback(_Deck);
                }
            } 
        }
        
        Action<Deck> OnDeckLoadedCallback;

        void Awake()
        {
            if (DeckSceneManager.Instance == null)
            {
                DeckSceneManager.Instance = this;
            }

            ResourceManager = gameObject.GetComponent<ResourceManager>();

            if ( Utils.isMissing("DeckSceneManager", new UnityEngine.Object[]{ ResourceManager, DeckEditController, DeckMenuController }) ) return;

            ResourceManager.Init();
            DeckEditController.Init(this);
            DeckMenuController.Init(this, ResourceManager);
        }

        public void RegisterOnDeckLoadedCallback(Action<Deck> callback)
        {
            OnDeckLoadedCallback += callback;
        }

        public void UnregisterOnDeckLoadedCallback(Action<Deck> callback)
        {
            OnDeckLoadedCallback -= callback;
        }
    }
}
