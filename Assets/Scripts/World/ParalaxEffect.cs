using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    [SerializeField] Vector2 effectStrength;
    [SerializeField] bool repeat;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private float startPosition;
    private float spriteWidth;

    private int indexPosition;
    private Vector3 worldStartPosition;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void Start()
    {
        startPosition = transform.position.x;
        worldStartPosition = transform.position;
        
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            spriteWidth = renderer.bounds.size.x;
            
            Debug.Log(spriteWidth);
        }
    }


    private void Update()
    {
        
        Vector3 deltaPosition = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaPosition.x * effectStrength.x, deltaPosition.y * effectStrength.y, 0f);
        lastCameraPosition = cameraTransform.position;



        if (!repeat) { return; }

        // INFINITE WORLD CURRENTLY A BIT BUGGY
        if (cameraTransform.position.x - transform.position.x >= spriteWidth*2 )
        {
            indexPosition+=3;            
            transform.position = new Vector3((indexPosition * spriteWidth) + worldStartPosition.x, worldStartPosition.y, worldStartPosition.z);
        }
        else if (transform.position.x - cameraTransform.position.x > spriteWidth*2 )
        {
            indexPosition-=3;            
            transform.position = new Vector3((indexPosition * spriteWidth) + worldStartPosition.x, worldStartPosition.y, worldStartPosition.z);
        }

        
        
       

    }
}
