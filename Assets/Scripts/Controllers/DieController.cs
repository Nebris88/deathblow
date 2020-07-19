using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class DieController : MonoBehaviour
    {
        public GameObject FreezeToggleGameObject;
        public GameObject LockToggleGameObject; 
        public GameObject RemoveButtonGameObject; 
        
        public Image DieImage { get; set; }
        public Button RollButton { get; set; }
        public Toggle FreezeToggle { get; set; }
        public Toggle LockToggle { get; set; }
        public Button RemoveButton { get; set; }

        public Die Die { get; set; }

        public void Init(Die die)
        {
            if ( Utils.isMissing("DieController", new Object[]{ FreezeToggleGameObject, LockToggleGameObject, RemoveButtonGameObject }) ) return;

            Die = die;

            DieImage = gameObject.GetComponent<Image>();
            RollButton = gameObject.GetComponent<Button>();
            FreezeToggle = FreezeToggleGameObject.GetComponent<Toggle>();
            LockToggle = LockToggleGameObject.GetComponent<Toggle>();
            RemoveButton = RemoveButtonGameObject.GetComponent<Button>();

            RollButton.onClick.AddListener(delegate { die.Roll(); });
            FreezeToggle.onValueChanged.AddListener(delegate { die.IsFrozen = FreezeToggle.isOn; });
            LockToggle.onValueChanged.AddListener(delegate { die.IsLocked = LockToggle.isOn; });
            RemoveButton.onClick.AddListener(delegate { Die.DieOwner.RemoveDie(Die); });

            die.RegisterOnDieChangedCallback(OnDiceChanged);
            OnDiceChanged(die);
        }

        public void TearDown()
        {
            Die.UnregisterOnDieChangedCallback(OnDiceChanged);
        }

        public void OnDiceChanged(Die die)
        {   
            DieImage.sprite = MasterManager.Instance.ResourceManager.GetSpriteByDieFace(die.DieFace);
            FreezeToggle.isOn = die.IsFrozen;
            LockToggle.isOn = die.IsLocked;
        }
    }
}
