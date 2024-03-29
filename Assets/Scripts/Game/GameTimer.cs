﻿namespace Assets.Scripts.Game
{
    public class GameTimer
    {

        private float targetTime;
        private float tickedTime;

        public void Tick(float secounds)
        {
            tickedTime += secounds;
        }

        public void SetTargetTime(float targetTime)
        {
            this.targetTime = targetTime;
        }

        public void Reset()
        {
            tickedTime = 0;
        }

        public bool IsDone()
        {
            return tickedTime >= targetTime;
        }

        public float getProgress()
        {
            return tickedTime / targetTime;
        }

        public float GetRemainingTime()
        {
            return targetTime - tickedTime;
        }

    }
}
