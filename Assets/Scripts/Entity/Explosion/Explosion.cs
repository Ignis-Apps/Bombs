using UnityEngine;

public class Explosion : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.PlaySound(SoundManager.Sound.EXPLOSION, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("explosionDone"))
        {
             Destroy(gameObject);
        }
    }
}
    