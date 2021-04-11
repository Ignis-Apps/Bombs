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

        if (collision.name.Contains("Crystal"))
        {
            // Destroy(collision.gameObject);      
            GameManager.GetInstance().OnCrystalCollected(1);
        }

        if (collision.CompareTag("Bomb"))
        {
            Destroy(collision.gameObject);
            if (isInvincible) { return; }

            gameManager.OnPlayerHit();
            if(gameManager.playerStats.Lifes == 0)
            {
                gameManager.OnPlayerDied();
            }
            
                   
            
        }

        //Debug.Log(collision.name);
       
      
    }

    public void OnWaveSurvived()
    {
        particles.Play();
    }

    public void ResetPose()
    {

    }


}
