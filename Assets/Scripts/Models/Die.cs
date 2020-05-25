﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public class Die
    {
        private DieFace _DieFace;
        private bool _IsFrozen;
        private bool _IsLocked;

        public DieOwner DieOwner { get; set; }
        public DieType DieType { get; set; }

        Action<Die> OnDieChangedCallback;

        public DieFace DieFace 
        { 
            get => _DieFace; 
            set 
            {
                bool change = (_DieFace != value);
                _DieFace = value;
                if (change && OnDieChangedCallback != null)
                {
                    OnDieChangedCallback(this);
                }
            } 
        }
        public bool IsFrozen 
        { 
            get => _IsFrozen; 
            set 
            {
                bool change = (_IsFrozen != value);
                _IsFrozen = value;
                if (change && OnDieChangedCallback != null)
                {
                    OnDieChangedCallback(this);
                }
            } 
        }
        public bool IsLocked
         { 
            get => _IsLocked; 
            set 
            {
                bool change = (_IsLocked != value);
                _IsLocked = value;
                if (change && OnDieChangedCallback != null)
                {
                    OnDieChangedCallback(this);
                }
            } 
        }

        public Die (DieOwner dieOwner, DieType dieType)
        {
            DieOwner = dieOwner;
            DieType = dieType;
            DieFace = Dice.GetDefaultFace(dieType);
            IsFrozen = false;
            IsLocked = false;
        }

        public void Roll()
        {
            DieFace = Dice.Roll(DieType);
            if (OnDieChangedCallback != null)
            {
                OnDieChangedCallback(this);
            }
        }

        public void RegisterOnDieChangedCallback(Action<Die> callback)
        {
            OnDieChangedCallback += callback;
        }

        public void UnregisterOnDieChangedCallback(Action<Die> callback)
        {
            OnDieChangedCallback -= callback;
        }
    }
}
