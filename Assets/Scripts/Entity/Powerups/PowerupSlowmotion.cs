using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.Powerups
{
    class PowerupSlowmotion : Powerup
    {
        [SerializeField] private float slowMotionFactor;
        
        private float timeScaleBefore;
        private float fixedDeltaTimeBefore;

        public override void OnPowerupActivate()
        {
            timeScaleBefore = Time.timeScale;
            fixedDeltaTimeBefore = Time.fixedDeltaTime;

            Time.timeScale /= slowMotionFactor ;
            Time.fixedDeltaTime /= slowMotionFactor;

            remaingTime /= slowMotionFactor;
            
        }

        public override void OnPowerupDeactivate()
        {
            Time.timeScale = timeScaleBefore;
            Time.fixedDeltaTime = fixedDeltaTimeBefore;
                
        }
    }
}
