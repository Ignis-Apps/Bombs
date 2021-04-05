using System;
using System.Collections;
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
        [SerializeField] private float transitionTime;
        
        private float timeScaleBefore;
        private float fixedDeltaTimeBefore;

        public override void OnPowerupActivate()
        {
            timeScaleBefore = Time.timeScale;
            fixedDeltaTimeBefore = Time.fixedDeltaTime;    
            remaingTime /= slowMotionFactor;

            StartCoroutine(SlowDownTime());
            
        }

        public override void OnPowerupDeactivate()
        {
            StartCoroutine(SpeedUpTime());
        }

        private IEnumerator SlowDownTime()
        {
            float targetTimeScale = Time.timeScale / slowMotionFactor;
            float targetFixedDeltaTime = Time.fixedDeltaTime / slowMotionFactor;

            float passedTime = 0;
            float progress;
            do
            {
                passedTime += Time.unscaledDeltaTime;
                progress = passedTime / transitionTime;
                Time.timeScale = Mathf.Lerp(timeScaleBefore, targetTimeScale, progress);
                Time.fixedDeltaTime = Mathf.Lerp(fixedDeltaTimeBefore, targetFixedDeltaTime,progress );
                yield return null;
            } while (progress < 1);            
        }

        private IEnumerator SpeedUpTime()
        {
            float startTimeScale = Time.timeScale;
            float startFixedDeltaTime = Time.fixedDeltaTime;

            float passedTime = 0;
            float progress;
            do
            {
                passedTime += Time.unscaledDeltaTime;
                progress = passedTime / transitionTime;
                Time.timeScale = Mathf.Lerp(startTimeScale, timeScaleBefore, progress);
                Time.fixedDeltaTime = Mathf.Lerp(startFixedDeltaTime, fixedDeltaTimeBefore, progress);
                yield return null;
            } while (progress < 1);
        }
    }
}
