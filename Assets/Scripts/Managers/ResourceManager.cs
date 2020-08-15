using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deathblow
{
    public class ResourceManager : MonoBehaviour
    {
        private const string DECK_PATH = "/deck.json";

        public Dictionary<string, Sprite> IconSprites { get; set; }
        
        public void Init()
        {
            loadSprites();
        }

        // Load all our sprites
        private void loadSprites()
        {
            IconSprites = new Dictionary<string, Sprite>();
            Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites");
            new List<Sprite>(sprites).ForEach(sprite => {
                IconSprites[sprite.name] = sprite;
            });
        }

        public Sprite GetSpriteByCardBonus(CardBonus cardBonus)
        {
            if (!IconSprites.ContainsKey(cardBonus.ToString()))
            {
                Debug.LogError("Missing a sprite? " + cardBonus.ToString());
                return null;
            }

            return IconSprites[cardBonus.ToString()];
        }

        public Sprite GetSpriteByDieFace(DieFace dieface)
        {
            if (!IconSprites.ContainsKey(dieface.ToString()))
            {
                Debug.LogError("Missing a sprite? " + dieface.ToString());
                return null;
            }

            return IconSprites[dieface.ToString()];
        }

        public Sprite GetSpriteByCharge(Charge charge)
        {
            if (!IconSprites.ContainsKey(charge.ToString()))
            {
                Debug.LogError("Missing a sprite? " + charge.ToString());
                return null;
            }

            return IconSprites[charge.ToString()];
        }

        public void SaveDeck(Deck deck)
        {
            Debug.Log("Deck Saved");
            DeckData deckData = new DeckData(deck);
            string deckDataJson = JsonUtility.ToJson(deckData);
            File.WriteAllText(Application.streamingAssetsPath + DECK_PATH, deckDataJson);
        }

        public Deck LoadDeck()
        {
            Debug.Log("Deck Loaded");
            string deckDataJson = File.ReadAllText(Application.streamingAssetsPath + DECK_PATH);
            DeckData deckData = JsonUtility.FromJson<DeckData>(deckDataJson);
            return new Deck(deckData);
        }
    }
}
