using Assets.Scripts.World.Sky; 
using UnityEngine;

[ExecuteInEditMode]
public class DynamicSky : MonoBehaviour
{
    [SerializeField] private SkyGradient skyBaseLayer;
    [SerializeField] private SkyGradient skyBlendLayer;
    
    [SerializeField] private float skyIndex;
    [SerializeField] private SkySettings skySettings;
    [SerializeField] private bool fetchIndexFromGamemanager;

    [SerializeField] private float timeStep;

    public float SkyIndex { get => skyIndex; set { skyIndex = value; } }

    private float previousSkyIndex;
    private GameManager gameManaegr;

    private void Awake()
    {
        gameManaegr = GameManager.GetInstance();
    }

    private void Start()
    {
        // Load initial sky state
        skyBaseLayer.overrideAlpha = true;
        skyBlendLayer.overrideAlpha = true;
        LoadSkyGradients();
        previousSkyIndex = skyIndex;
        
    }

    private void LateUpdate()
    {
        if (fetchIndexFromGamemanager && gameManaegr!= null)
        {
            skyIndex = gameManaegr.DayTime;
        }        

        if(Mathf.Abs(skyIndex - previousSkyIndex) > 0)
        {
            if(Mathf.Floor(skyIndex) == Mathf.Floor(previousSkyIndex))
            {
                skyBlendLayer.forcedAlphaValue = mod(skyIndex, 1f);
            }
            else
            {
                LoadSkyGradients();
            }
            previousSkyIndex = skyIndex;         
        }
    }

    private void LoadSkyGradients()
    {
        skyBaseLayer.SetGradient(skySettings.GetSkyGradient(skyIndex));
        skyBaseLayer.forcedAlphaValue = 1f;

        skyBlendLayer.SetGradient(skySettings.GetSkyGradient(skyIndex + 1));
        skyBlendLayer.forcedAlphaValue = mod(skyIndex, 1f);
    }

   // True modulo function 
    private float mod(float a, float b)
    {
        return a - b * Mathf.Floor(a / b);
    }

}
