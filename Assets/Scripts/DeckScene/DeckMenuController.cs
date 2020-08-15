using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class DeckMenuController : MonoBehaviour
    {
        public Button LoadDeckButton;
        public Button SaveDeckButton;

        public void Init(DeckSceneManager deckSceneManager, ResourceManager resourceManager)
        {
            if ( Utils.isMissing("DeckMenuController", new Object[]{ LoadDeckButton, SaveDeckButton }) ) return;

            LoadDeckButton.onClick.AddListener(delegate { deckSceneManager.Deck = resourceManager.LoadDeck(); });
            SaveDeckButton.onClick.AddListener(delegate { resourceManager.SaveDeck(deckSceneManager.Deck); });
            
            deckSceneManager.Deck = resourceManager.LoadDeck();
        }
    }
}