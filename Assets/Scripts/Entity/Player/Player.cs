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
        gameManager.Player = gameObject;
        controller = GetComponent<MovementController>();
                
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name.Contains("Coin"))
        {           
            GameManager.GetInstance().OnCoinCollected(1);                
        }

        if (collision.name.Contains("ScoreOrb"))
        {                
            GameManager.GetInstance().OnPointCollected(1);
        }

        if (collision.name.Contains("Crystal"))
        {            
            GameManager.GetInstance().OnCrystalCollected(1);
        }

        if (collision.CompareTag("Bomb"))
        {            
            Destroy(collision.gameObject);
            if (isInvincible) { return; }

            gameManager.OnPlayerHit();
            if(gameManager.playerStats.Lifes == 0 && ScreenManager.GetInstance().CanPlayerMove())
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
