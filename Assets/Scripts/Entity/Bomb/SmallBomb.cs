using Assets.Scripts.Entity.Bomb;

public class SmallBomb : Bomb

{
    public override float GetStartSpeed()
    {
        return gameManager.CurrentWave.GetSmallBombInitialSpeed(gameManager.session.progressStats.currentWaveProgress);
    }
}
