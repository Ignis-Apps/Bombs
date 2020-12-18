using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI ScoreText;

    private int Coins;
    private int Points;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CoinText.text = Coins.ToString();
        ScoreText.text = Points.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name.Contains("Coin"))
        {
            Destroy(collision.gameObject);
            Coins++;
        }

        if (collision.name.Contains("ScoreOrb"))
        {
            Destroy(collision.gameObject);
            Points+=Random.Range(2,7);
        }

        Debug.Log(collision.name);
       
      
    }


}
