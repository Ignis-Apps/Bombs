using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entity.Bomb
{
    class DefaultBomb : Bomb
    {
        public override float GetStartSpeed()
        {
           return gameManager.CurrentWave.GetDefaultBombInitialSpeed(gameManager.CurrentWaveProgress);
        }
    }
}
