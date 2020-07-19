using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class DeckManager : MonoBehaviour
    {
        public DeckController DeckController;

        public void Init(GameManager gameManager)
        {
            if ( Utils.isMissing("DeckManager", new Object[]{ DeckController }) ) return;

            DeckController.Init(gameManager);
        }
    }
}
