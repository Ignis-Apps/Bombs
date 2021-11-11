namespace Assets.Scripts.Entity.Bomb
{
    public class DefaultBomb : Bomb
    {
        public override float GetStartSpeed()
        {
           return gameManager.CurrentWave.GetDefaultBombInitialSpeed(gameManager.session.progressStats.currentWaveProgress);
        }
    }
}
