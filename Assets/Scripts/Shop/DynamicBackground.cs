using UnityEngine;

[RequireComponent(typeof(Background))]
[ExecuteInEditMode]
public class DynamicBackground : MonoBehaviour
{
    [SerializeField]
    private Gradient[] skyGradients;

    private Background sky;

    [SerializeField]
    private Background skyOverlay;

    private int skyCounter;
    private int skyOverlayCounter;

    private float currentTime;
    private float lastTime;
    private GameManager gameManager;

    private Vector2 lastTouchPos;

    //private Gradient targetGradient;
    // Start is called before the first frame update
    void Start()
    {
        sky = GetComponent<Background>();

        skyCounter = 0;
        skyOverlayCounter = 1;

        lastTime = 0;
        currentTime = 0;

        gameManager = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.touchCount > 0)
        {
            //Debug.Log(currentTime);
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //Debug.Log("Began");
                lastTouchPos = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                //Debug.Log("Moved");
                float diff = (Input.GetTouch(0).position.x - lastTouchPos.x) / (float) Screen.width;
                currentTime += diff;
                lastTouchPos = Input.GetTouch(0).position;
            }
        } 
        else if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Began");
                lastTouchPos = Input.mousePosition;
            }
            else
            {
                //Debug.Log("Moved");
                float diff = (Input.mousePosition.x - lastTouchPos.x) / (float)Screen.width;
                currentTime -= diff;
                if(currentTime < 0)
                {
                    currentTime = 0;
                }
                lastTouchPos = Input.mousePosition;
            }
        }
        else
        {
            float nearestHoleNumber = Mathf.Round(currentTime);

            if(Mathf.Abs(currentTime - nearestHoleNumber) < 0.005f)
            {
                currentTime = nearestHoleNumber;
            }
            else
            {
                currentTime += (nearestHoleNumber - currentTime) / 40;
            }
        }
            

        
        if(((int)currentTime) - ((int)lastTime) != 0)
        {
            if(((int)currentTime) - ((int)lastTime) > 0)
            {
                if (skyCounter == 4) { skyCounter = 0; } else { skyCounter++; }
                if (skyOverlayCounter == 4) { skyOverlayCounter = 0; } else { skyOverlayCounter++; }
            }
            else
            {
                if (skyCounter == 0) { skyCounter = 4; } else { skyCounter--; }
                if (skyOverlayCounter == 0) { skyOverlayCounter = 4; } else { skyOverlayCounter--; }
            }

            sky.gradient = skyGradients[skyCounter];
            skyOverlay.gradient = skyGradients[skyOverlayCounter];

            for (int i = 0; i < skyOverlay.gradient.alphaKeys.Length; i++)
            {
                skyOverlay.gradient.alphaKeys[i].alpha = 0;
            }
        } 
        else
        {
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[skyOverlay.gradient.alphaKeys.Length];

            for (int i = 0; i < alphaKeys.Length; i++)
            {
                alphaKeys[i] = new GradientAlphaKey(currentTime - ((int)currentTime), skyOverlay.gradient.alphaKeys[i].time);
            }

            skyOverlay.gradient.SetKeys(skyOverlay.gradient.colorKeys, alphaKeys);
        }

        lastTime = currentTime;

        
        //currentTime += 0.0001f;
        
        // Nicht optimal, man könnte es auch nur machen wenn sich tatsächlich was ändert
        sky.manuellUpdate();
        skyOverlay.manuellUpdate();
    }
}
