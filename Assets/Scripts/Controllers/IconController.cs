using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class IconController : MonoBehaviour
    {
        public GameObject FrozenObject { get; set; }

        public void Init(GameObject frozenPrefab)
        {
            FrozenObject = GameObject.Instantiate(frozenPrefab);
            FrozenObject.transform.SetParent(transform);
            FrozenObject.SetActive(false);
        }

        public void SetFrozen(bool isFrozen)
        {
            FrozenObject.SetActive(isFrozen);
        }
    }
}
