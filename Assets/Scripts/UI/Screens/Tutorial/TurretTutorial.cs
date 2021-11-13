using Assets.Scripts.Game.Session;
using Assets.Scripts.UI.Screens.Tutorial;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurretTutorial : AbstractTutorial
{
    [SerializeField] private GameObject TurretPowerUp;

    [SerializeField] private Button OkButton;
    [SerializeField] private GameObject Dialog;
    [SerializeField] private GameObject MessageShoot;

    private int remainingBombs;
    private float previousTimeScale;

    public override void Init()
    {
        OkButton.onClick.AddListener(OnDialogClicked);
        Dialog.SetActive(false);
        MessageShoot.SetActive(false);
        GameSessionEventHandler.bombAvoidedDelegate += OnBombAvoided;
        GameSessionEventHandler.powerUpCollected += OnPowerUpCollected;
    }

    private void OnDialogClicked()
    {
        Dialog.SetActive(false);
       // Time.timeScale = previousTimeScale;
        StartCoroutine(RunTutorial());
    }

    private void OnBombAvoided()
    {
        remainingBombs--;
    }

    private void OnPowerUpCollected(Powerup p) {
             
        p.DeactivatePowerup();        
        Dialog.SetActive(true);
        GameSessionEventHandler.powerUpCollected -= OnPowerUpCollected;
    }

    private IEnumerator RunTutorial()
    {
        Instantiate(TurretPowerUp, gameManager.getPlayer().transform);        
        yield return new WaitForSeconds(3f);
        MessageShoot.SetActive(true);

        // constant
        float turretLifetime = 15f;
        float spawnInterval = 1f;
        
        while(turretLifetime > 3f)
        {
            turretLifetime -= spawnInterval;
            yield return new WaitForSeconds(spawnInterval);
            gameManager.waveSpawner.SpawnBombAtRandomPosititon();
            remainingBombs++;

        }

        yield return new WaitUntil(() => remainingBombs <= 0);
        MessageShoot.SetActive(false);
        CompleteTutorial();

    }

    public override void Dispose()
    {
        GameSessionEventHandler.bombAvoidedDelegate -= OnBombAvoided;
        GameSessionEventHandler.powerUpCollected -= OnPowerUpCollected;
    }

    protected override void OnTutorialComplete()
    {
        //throw new System.NotImplementedException();
    }

    protected override void OnTutorialStart()
    {
        //previousTimeScale = Time.timeScale;
       // Time.timeScale = 0f;
    }
}
