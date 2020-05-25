﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class DieController : MonoBehaviour
    {
        public GameObject TextGameObject;
        public GameObject FreezeToggleGameObject;
        public GameObject LockToggleGameObject; 
        
        public Button RollButton { get; set; }
        public Text Text { get; set; }
        public Toggle FreezeToggle { get; set; }
        public Toggle LockToggle { get; set; }

        public Die Die { get; set; }

        public void Init(Die die)
        {
            if (TextGameObject == null)
            {
                Debug.LogError("Missing TextGameObject");
                return;
            }
            if (FreezeToggleGameObject == null)
            {
                Debug.LogError("Missing FreezeToggleGameObject");
                return;
            }
            if (LockToggleGameObject == null)
            {
                Debug.LogError("Missing LockToggleGameObject");
                return;
            }

            Die = die;

            RollButton = gameObject.GetComponent<Button>();
            Text = TextGameObject.GetComponent<Text>();
            FreezeToggle = FreezeToggleGameObject.GetComponent<Toggle>();
            LockToggle = LockToggleGameObject.GetComponent<Toggle>();

            RollButton.onClick.AddListener(delegate { die.Roll(); });
            FreezeToggle.onValueChanged.AddListener(delegate { die.IsFrozen = FreezeToggle.isOn; });
            LockToggle.onValueChanged.AddListener(delegate { die.IsLocked = LockToggle.isOn; });

            die.RegisterOnDieChangedCallback(OnDiceChanged);
            OnDiceChanged(die);
        }

        public void TearDown()
        {
            Die.UnregisterOnDieChangedCallback(OnDiceChanged);
        }

        public void OnDiceChanged(Die die)
        {   
            Text.text = die.DieFace.ToString();
            FreezeToggle.isOn = die.IsFrozen;
            LockToggle.isOn = die.IsLocked;
        }
    }
}
