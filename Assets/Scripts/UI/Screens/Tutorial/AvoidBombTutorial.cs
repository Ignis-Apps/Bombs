using Assets.Scripts.Game.Session;
using Assets.Scripts.UI.Screens.Tutorial;
using System.Collections;
using UnityEngine;

public class AvoidBombTutorial : AbstractTutorial
{
    [SerializeField] private GameObject BombPrefab;
    
    private int remainingBombs;

    public override void Init()
    {
        GameSessionEventHandler.bombAvoidedDelegate += OnBombAvoided;
    }
  
    public override void Dispose()
    {
        GameSessionEventHandler.bombAvoidedDelegate -= OnBombAvoided;
    }

    protected override void OnTutorialComplete()
    {        
    }

    protected override void OnTutorialStart()
    {        
        StartCoroutine(RunTutorial());
    }

    IEnumerator RunTutorial()
    {
        remainingBombs = 0;
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < 3; i++)
        {
            SpawnBombAbovePlayer();
            yield return new WaitForSeconds(1.9f);
        }

        yield return new WaitUntil(() => remainingBombs <= 0);
        yield return new WaitForSeconds(.5f);

        CompleteTutorial();
    }

    private void SpawnBombAbovePlayer()
    {
        GameObject bomb = Instantiate(BombPrefab);
        Vector3 position = gameManager.getPlayer().transform.position;
        position.y = 4f;
        bomb.transform.position = position;
        remainingBombs++;
    }

    private void OnBombAvoided()
    {
        remainingBombs--;
    }


}
