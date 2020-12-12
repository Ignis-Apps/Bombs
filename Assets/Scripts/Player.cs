using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    public TextMeshProUGUI CoinText;
    private int Coins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CoinText.text = Coins.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name.Contains("Coin"))
        {
            Destroy(collision.gameObject);
            Coins++;
        }

        Debug.Log(collision.name);
       
      
    }


}
