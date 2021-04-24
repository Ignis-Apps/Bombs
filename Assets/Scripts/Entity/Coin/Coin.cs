using UnityEngine;

public class Coin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D coinBody = GetComponent<Rigidbody2D>();
        coinBody.angularVelocity = Random.Range(-45f, 45f);
    }


}
