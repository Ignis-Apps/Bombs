﻿using Assets.Scripts.Game.Session;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private bool isInvincible;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] public PlayerSkinChanger PlayerSkinChanger;


    private MovementController controller;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
        gameManager.PlayerObject = gameObject;
        controller = GetComponent<MovementController>();
                
    }

    private void OnEnable()
    {
        GameSessionEventHandler.waveCompleteDelegate += OnWaveSurvived;
        GameSessionEventHandler.sessionResetDelegate += OnGameReset;
        GameSessionEventHandler.playerDiedDelegate += OnPlayerDied;
        GameSessionEventHandler.playerRevivedDelegate += OnPlayerRevived;
    }

    private void OnDisable()
    {
        GameSessionEventHandler.waveCompleteDelegate -= OnWaveSurvived;
        GameSessionEventHandler.sessionResetDelegate -= OnGameReset;
        GameSessionEventHandler.playerDiedDelegate -= OnPlayerDied;
        GameSessionEventHandler.playerRevivedDelegate -= OnPlayerRevived;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name.Contains("Coin"))                          
            GameSessionEventHandler.coinColltedDelegate();
        
        if (collision.name.Contains("Crystal"))
            GameSessionEventHandler.crystalColltedDelegate();

        if (collision.name.Contains("Marker"))
        {
            if(GameSessionEventHandler.targetMarkerReachedDelegate != null)
            {
                GameSessionEventHandler.targetMarkerReachedDelegate();
            }
            else
            {
                Debug.LogWarning("DELEGATE IS NULL");
            }
        }
            

        if (collision.CompareTag("Bomb"))
        {            
            Destroy(collision.gameObject);
            
            if (isInvincible)  
                return; 
            
            GameSessionEventHandler.playerHitDelegate();                                
            
        }
      
    }

    private void OnWaveSurvived()
    {
        particles.Play();
    }

    private void OnPlayerDied()
    {
        HideAndDisableMovement();
    }

    private void OnPlayerRevived()
    {
        //----------
        gameManager.session.playerStats.Lifes += 1;
        gameManager.session.playerStats.AmountOfRevives += 1;
        gameManager.session.playerStats.IsProtected = false;
        //-----------

        ShowAndEnableMovement();
    }

    private void OnGameReset()
    {
        // Set to center
        transform.position = new Vector2(0, transform.position.y);

        ShowAndEnableMovement();
    }

    private void HideAndDisableMovement()
    {
        GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(renderer => renderer.enabled = false);
        GetComponent<MovementController>().Stop();
    }

    private void ShowAndEnableMovement()
    {
        GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(renderer => renderer.enabled = true);
        GetComponent<MovementController>().enabled = true;
    }

}
