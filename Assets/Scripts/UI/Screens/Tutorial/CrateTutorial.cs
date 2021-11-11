using Assets.Scripts.Game.Session;
using Assets.Scripts.UI.Screens.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateTutorial : AbstractTutorial
{
    [SerializeField] private GameObject CratePrefab;
    [SerializeField] private GameObject CrateDropPrefab;

    [SerializeField] private GameObject messageLookCrate;
    [SerializeField] private GameObject messageOpenCrate;

    private bool crateHasLanded;
    private bool crateHasBeenOpened;

    public override void Init()
    {
        messageLookCrate.SetActive(false);
        messageOpenCrate.SetActive(false);

        GameSessionEventHandler.crateLandedDelegate += OnCrateLanded;        
        GameSessionEventHandler.crateOpenedDelegate += OnCrateOpened;        
    }

    public override void Dispose()
    {        
        GameSessionEventHandler.crateLandedDelegate -= OnCrateLanded;
        GameSessionEventHandler.crateOpenedDelegate -= OnCrateOpened;
    }

    private void OnCrateLanded()
    {
        crateHasLanded = true;
    }

    private void OnCrateOpened()
    {
        crateHasBeenOpened = true;
    }

    IEnumerator RunTutorial()
    {
        // Constants
        float distanceToPlayer = .5f;

        // Spawn crate
        GameObject crate = Instantiate(CratePrefab);
        Vector3 spawnPosition = gameManager.getPlayer().transform.position;
        // Crate spawns on the side closer to the origin of the map
        spawnPosition.x = (Mathf.Abs(spawnPosition.x + distanceToPlayer) < Mathf.Abs(spawnPosition.x - distanceToPlayer)) ?
            (spawnPosition.x + distanceToPlayer) : (spawnPosition.x - distanceToPlayer);
        spawnPosition.y = 4f;
        crate.GetComponent<Crate>().SetCrateDrop(CrateDropPrefab);
        crate.transform.position = spawnPosition;

        // Show "Oh look message"
        messageLookCrate.SetActive(true);
        yield return new WaitUntil(() => crateHasLanded);
        messageLookCrate.SetActive(false);

        // Show "Open crate message"
        messageOpenCrate.SetActive(true);
        yield return new WaitUntil(() => crateHasBeenOpened);
        messageOpenCrate.SetActive(false);

        // Show "Powerups"

        CompleteTutorial();
        
    }

    protected override void OnTutorialComplete()
    {        
    }

    protected override void OnTutorialStart()
    {
        StartCoroutine(RunTutorial());
    }
}
