using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deathblow
{
    public class MenuManager : MonoBehaviour
    {
        public Button NewGameButton;

        public void Init(GameManager gameManager)
        {
            if ( Utils.isMissing("DeckManager", new Object[]{ NewGameButton }) ) return;

            NewGameButton.onClick.AddListener(delegate { gameManager.StartNewGame(); });
        }
    }
}