using Assets.Scripts.Entity.Bomb;

public class Bomb_Small : Bomb

{
    public override float GetStartSpeed()
    {
        return gameManager.CurrentWave.GetSmallBombInitialSpeed(gameManager.CurrentWaveProgress);
    }
}
