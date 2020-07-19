using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Deathblow
{
    public static class Utils
    {
        public static bool isMissing(string caller, Object[] objs)
        {
            bool taco = false;
            foreach (Object obj in objs)
            {
                if (obj == null)
                {
                    Debug.LogError($"{caller} is missing something!");
                    taco = true;
                }
            }
            return taco;
        }
    }
}
