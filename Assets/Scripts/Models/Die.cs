using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public class Die
    {
        public DieOwner DieOwner { get; set; }
        public DieType DieType { get; set; }

        Action<Die> OnDieChangedCallback;

        private DieFace _DieFace;
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

        private bool _IsFrozen;
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

        private bool _IsLocked;
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
