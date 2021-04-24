using UnityEngine;

public class BoundaryCheck : MonoBehaviour
{
    private Vector2 screenBound;
    private float playerWidth;

    // Start is called before the first frame update
    void Start()
    {
        screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        playerWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPositon = transform.position;
        viewPositon.x = Mathf.Clamp(viewPositon.x, -screenBound.x + playerWidth, screenBound.x - playerWidth);
        transform.position = viewPositon;
    }
}
