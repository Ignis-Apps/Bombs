using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game
{
    class GameData : Singleton<GameData>
    {
        private int highScore;
        private int coinBalance;

        public int HighScore { get => highScore; set { if (value > highScore) { highScore = value; } } }
        public int CoinBalance { get => coinBalance; set { coinBalance = value; } }
        
         /*
            Load/Save - Sollte man anderst machen aber im Moment reicht es.            
         */

        public void LoadData() {

            highScore = PlayerPrefs.GetInt("highScore");
            coinBalance = PlayerPrefs.GetInt("coinBalance");
        
        }
        public void SaveData() {

            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.SetInt("coinBalance", coinBalance);

        }

    }
}
