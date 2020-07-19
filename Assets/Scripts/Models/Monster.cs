using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public class Monster : Entity
    {
        public Dictionary<Charge, Ability> Abilities { get; set; }
        public Ability Special { get; set; }

        public Monster(string name)
        {
            Name = name;
            Dice = new List<Die>();
            Charges = new Dictionary<Charge, int>();
            Charges.Add(Charge.Monster_1, 0);
            Charges.Add(Charge.Monster_2, 0);
            Charges.Add(Charge.Monster_3, 0);
            Abilities = new Dictionary<Charge, Ability>();
            Abilities.Add(Charge.Monster_1, new Ability("Ability 1", "Do Stuff", 1));
            Abilities.Add(Charge.Monster_2, new Ability("Ability 2", "Do Stuff", 2));
            Abilities.Add(Charge.Monster_3, new Ability("Ability 3", "Do Stuff", 3));
            Special = new Ability("Special","Do Stuff");
        }
    }

    public class Ability
    {
        public string Name { get; set; }
        public string Effect { get; set; }
        public int Charge { get; set; }

        public Ability (string name, string effect, int charge = 0)
        {
            Name = name;
            Effect = effect;
            Charge = charge;
        }
    }
}
