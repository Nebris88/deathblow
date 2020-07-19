using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class OverviewController : MonoBehaviour
    {
        public Text LabelText;
        public GameObject IconPrefab;
        public GameObject IconPanel; 
        public GameObject FrozenPrefab;

        public Entity Entity { get; set; }

        public Dictionary<Die, GameObject> DieIcons { get; set; }
        public Dictionary<Charge, GameObject> ChargeIcons { get; set; }

        public void Init(Entity entity)
        {
            if ( Utils.isMissing("OverviewController", new Object[]{ LabelText, IconPrefab, IconPanel, FrozenPrefab }) ) return;

            Entity = entity;
            LabelText.text = Entity.Name;

            DieIcons = new Dictionary<Die, GameObject>();
            ChargeIcons = new Dictionary<Charge, GameObject>();

            Entity.RegisterOnDieAddedCallback(OnDieAdded);
            Entity.RegisterOnDieRemovedCallback(OnDieRemoved);
        }

        //Dice
        public void SetActiveIdentifier(bool active)
        {
            LabelText.text = (active ? "> " : "") + Entity.Name;
        }

        public void OnDieAdded(Die die)
        {

            GameObject iconObject = GameObject.Instantiate(IconPrefab);
            iconObject.transform.SetParent(IconPanel.transform);
            iconObject.name = Entity.Name + "'s Die";
            iconObject.GetComponent<Image>().sprite = MasterManager.Instance.ResourceManager.GetSpriteByDieFace(die.DieFace);
            iconObject.AddComponent<IconController>().Init(FrozenPrefab);

            die.RegisterOnDieChangedCallback(OnDieChanged);

            DieIcons.Add(die, iconObject);
        }

        public void OnDieRemoved(Die die)
        {
            if (!DieIcons.ContainsKey(die))
            {
                Debug.LogError("Trying to remove die icon from overview panel that doesnt have an icon for said die.");
                return;
            }

            die.UnregisterOnDieChangedCallback(OnDieChanged);

            GameObject iconObject = DieIcons[die];
            DieIcons.Remove(die);
            Destroy(iconObject);
        }

        public void OnDieChanged(Die die)
        {
            if (!DieIcons.ContainsKey(die))
            {
                Debug.LogError("Trying to change die icon from overview panel that doesnt have an icon for said die.");
                return;
            }
            DieIcons[die].GetComponent<Image>().sprite = MasterManager.Instance.ResourceManager.GetSpriteByDieFace(die.DieFace);
            DieIcons[die].GetComponent<IconController>().SetFrozen(die.IsFrozen);
        }
    }
}
