using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game
{
    class GameData : Singleton<GameData>
    {
        private int highScore;
        private int coinBalance;

        public int HighScore { get => highScore; set { if (value > highScore) { highScore = value; } } }
        public int CoinBalance { get => coinBalance; set { coinBalance = value; } }
        

        public void LoadData() { }
        public void SaveData() { }

    }
}
