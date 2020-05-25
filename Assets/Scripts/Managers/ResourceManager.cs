using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deathblow
{
    public class ResourceManager : MonoBehaviour
    {
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
    }
}
