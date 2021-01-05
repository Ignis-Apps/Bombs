using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkyGradient))] [ExecuteInEditMode]
public class DayNightCycle : MonoBehaviour
{
    [SerializeField]
    private Gradient[] skyGradients;
    [SerializeField][Range(0,1)]
    private float time;
    [SerializeField]
    private bool fetchTimeFromGameManager;
    [SerializeField]
    private bool forceRedraw;

    private float currentTime;
    private GameManager gameManager;

    private Gradient targetGradient;
    // Start is called before the first frame update
    void Start()
    {
        targetGradient = GetComponent<SkyGradient>().gradient;
        gameManager = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (fetchTimeFromGameManager)
        {
            // Throws an error in editor cause the game manager doesnt exist there
            if(gameManager.getDaytime() != currentTime || forceRedraw)
            {
                setGradient(gameManager.getDaytime());
                
            }
        }
        else
        {
              if(time != currentTime || forceRedraw)
              {
               setGradient(time);
               }
        }

      
    }

    private void setGradient(float time)
    {
        int index = Mathf.RoundToInt(time * (skyGradients.Length - 1));
        targetGradient.SetKeys(skyGradients[index].colorKeys, skyGradients[index].alphaKeys);
        currentTime = time;
    }
}
