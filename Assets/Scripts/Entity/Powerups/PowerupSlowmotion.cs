using Assets.Scriptable.powerups;
using Assets.Scriptable.powerups.techtree;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Entity.Powerups
{
    class PowerupSlowmotion : Powerup
    {
        [SerializeField] private SlowMotionUpgrade slowDownUpgradeTree;        
        [SerializeField] private float transitionTime;
        
        private float timeScaleBefore;
        private float fixedDeltaTimeBefore;

        private SlowMotionConfiguration configuration;

        public override PowerUpConfiguration LoadConfiguration()
        {   
            configuration = slowDownUpgradeTree.GetSlowMotionConfiguration();      
            return configuration;
        }

        public override void OnPowerupActivate()
        {
            timeScaleBefore = Time.timeScale;
            fixedDeltaTimeBefore = Time.fixedDeltaTime;    
            remaingTime /= configuration.SlowMotionFactor;

            StartCoroutine(SlowDownTime());
            
        }

        public override void OnPowerupDeactivate()
        {
            StartCoroutine(SpeedUpTime());
        }

        private IEnumerator SlowDownTime()
        {
            float targetTimeScale = Time.timeScale / configuration.SlowMotionFactor;
            float targetFixedDeltaTime = Time.fixedDeltaTime / configuration.SlowMotionFactor;

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
