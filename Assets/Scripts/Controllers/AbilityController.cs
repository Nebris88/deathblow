using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class AbilityController : MonoBehaviour
    {
        public Text AbilityNameText;
        public Text AbilityEffectText;
        public GameObject CardIconPanel; 
        public GameObject CardIconPrefab;

        public Ability Ability { get; set; }

        public void Init(Ability ability)
        {
            if ( Utils.isMissing("CardController", new Object[]{ AbilityNameText, AbilityEffectText, CardIconPanel, CardIconPrefab }) ) return;

            Ability = ability;
            AbilityNameText.text = ability.Name;
            AbilityEffectText.text = ability.Effect;

            Sprite sprite = null;

            switch (ability.Charge)
            {
                case 1:
                    sprite = MasterManager.Instance.ResourceManager.GetSpriteByDieFace(DieFace.Monster_1);
                    break;
                case 2:
                    sprite = MasterManager.Instance.ResourceManager.GetSpriteByDieFace(DieFace.Monster_2);
                    break;
                case 3:
                    sprite = MasterManager.Instance.ResourceManager.GetSpriteByDieFace(DieFace.Monster_3);
                    break;
            }

            for (int x = 0; x < ability.Charge; x++)
            {
                if (sprite != null)
                {
                    GameObject cardIconObject = GameObject.Instantiate(CardIconPrefab);
                    cardIconObject.transform.SetParent(CardIconPanel.transform);
                    cardIconObject.name = ability.Name;
                    cardIconObject.GetComponent<Image>().sprite = sprite;
                }
            }
        }
    }
}
