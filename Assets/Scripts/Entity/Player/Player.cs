using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private bool isInvincible;
    [SerializeField] private ParticleSystem particles;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
        gameManager.Player = gameObject;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name.Contains("Coin"))
        {
           // Destroy(collision.gameObject);
            GameManager.GetInstance().OnCoinCollected(1);
            
    
        }

        if (collision.name.Contains("ScoreOrb"))
        {
           // Destroy(collision.gameObject);      
            GameManager.GetInstance().OnPointCollected(1);
        }

        if (collision.CompareTag("Bomb"))
        {
            Destroy(collision.gameObject);
            if (isInvincible) { return; }
            if(gameManager.PlayerLifes > 1)
            {
                gameManager.OnPlayerHit();
                return;
            }

            gameManager.OnPlayerDied();
            GameMenuManager.GetInstance().SwitchController(GameMenu.GAME_OVER_SCREEN);
        }

        Debug.Log(collision.name);
       
      
    }

    public void OnWaveSurvived()
    {
        particles.Play();
    }


}
