using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraManager : MonoBehaviour
{
    [SerializeField] private const float aspectRatioThreshold = 9f/16f;
    [SerializeField] private float defaultSize;

    private Camera cam;
    private float aspectRatio;

    private float originalAspectRatio;
    private float originalOrthograficSize;
    private Vector3 originalCameraPosition;

    void Awake()
    {
        cam = GetComponent<Camera>();
        
        // Just to keep the editor clean
        cam.orthographicSize = defaultSize;
        
        originalAspectRatio = cam.aspect;
        originalOrthograficSize = cam.orthographicSize;
        originalCameraPosition = cam.transform.position;

        fitCameraToScreen();
    }

    // Update is called once per frame
    void Update()
    {
        // Only triggers in Unity since i cant change the aspect ratio of a physical deivce
        if(aspectRatio != cam.aspect)
        {
            fitCameraToScreen();
        }
    }

    private void fitCameraToScreen()
    {
           
        aspectRatio = cam.aspect;
        
        if (aspectRatio > aspectRatioThreshold) {

            cam.orthographicSize = originalOrthograficSize;
            cam.transform.position = originalCameraPosition;

            return;
        }

        cam.orthographicSize = (originalOrthograficSize / aspectRatio) * aspectRatioThreshold;
        Vector3 camPos = originalCameraPosition;
        camPos.y = cam.orthographicSize - originalOrthograficSize;
        cam.transform.position = camPos;
    }

    public void resetCameraPosition()
    {
        fitCameraToScreen();
    }
}
