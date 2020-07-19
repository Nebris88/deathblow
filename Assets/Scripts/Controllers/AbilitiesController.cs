using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class AbilitiesController : MonoBehaviour
    {
        public GameObject AbilityPrefab;

        public void Init(Monster monster)
        {
            if ( Utils.isMissing("CardController", new Object[]{ AbilityPrefab }) ) return;

            MakeAbility(monster.Special);
            monster.Abilities.Keys.ToList().ForEach(charge => {
                MakeAbility(monster.Abilities[charge]);
            });
        }

        private void MakeAbility(Ability ability)
        {
            GameObject abilityObject = GameObject.Instantiate(AbilityPrefab);
            abilityObject.transform.SetParent(transform);
            abilityObject.name = ability.Name;
            abilityObject.GetComponentInChildren<AbilityController>().Init(ability);
        }
    }
}
