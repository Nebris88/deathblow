using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deathblow
{
    public enum DieType { Standard, Monster }  
    public enum DieFace { Attack, Defense, Heal, Power, Mind, Life, Monster_1, Monster_2, Monster_3}

    public static class Dice
    {
        private static Dictionary<DieType, DieFace[]> DiceSet;

        private static void InitDice()
        {
            if (DiceSet == null)
            {
                DiceSet = new Dictionary<DieType, DieFace[]>();

                DieFace[] standardDieFaces = { DieFace.Attack, DieFace.Defense, DieFace.Heal, DieFace.Power, DieFace.Mind, DieFace.Life };
                DieFace[] monsterDieFaces = { DieFace.Attack, DieFace.Attack, DieFace.Defense, DieFace.Monster_1, DieFace.Monster_2, DieFace.Monster_3 };

                DiceSet.Add(DieType.Standard, standardDieFaces);
                DiceSet.Add(DieType.Monster, monsterDieFaces);
            }
        }

        public static DieFace Roll(DieType dieType)
        {
            InitDice();
            return DiceSet[dieType][(int)Random.Range(0, 6)];
        }

        public static DieFace GetDefaultFace(DieType dieType)
        {
            InitDice();
            return DiceSet[dieType][0];
        }

        public static DieFace[] GetDieFaces(DieType dieType)
        {
            InitDice();
            return DiceSet[dieType];
        }
    }
}
