using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public class Monster : Entity
    {
        public Monster(string name)
        {
            Name = name;
            Dice = new List<Die>();
            Charges = new Dictionary<Charge, int>();
            Charges.Add(Charge.Monster_1, 0);
            Charges.Add(Charge.Monster_2, 0);
            Charges.Add(Charge.Monster_3, 0);
        }
    }
}
