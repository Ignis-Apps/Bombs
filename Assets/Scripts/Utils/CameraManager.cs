using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private const float aspectRatioThreshold = 9f/16f;

    [SerializeField]
    private float defaultSize;

    private Camera camera;
    private float aspectRatio;

    private float originalAspectRatio;
    private float originalOrthograficSize;
    private Vector3 originalCameraPosition;

    void Start()
    {
        camera = GetComponent<Camera>();
        
        // Just to keep the editor clean
        camera.orthographicSize = defaultSize;
        
        originalAspectRatio = camera.aspect;
        originalOrthograficSize = camera.orthographicSize;
        originalCameraPosition = camera.transform.position;

        fitCameraToScreen();
    }

    // Update is called once per frame
    void Update()
    {
        // Only triggers in Unity since i cant change the aspect ratio of a physical deivce
        if(aspectRatio != camera.aspect)
        {
            fitCameraToScreen();
        }
    }

    private void fitCameraToScreen()
    {
           
        aspectRatio = camera.aspect;
        
        if (aspectRatio > aspectRatioThreshold) {

            camera.orthographicSize = originalOrthograficSize;
            camera.transform.position = originalCameraPosition;

            return;
        }

        camera.orthographicSize = (originalOrthograficSize / aspectRatio) * aspectRatioThreshold;
        Vector3 camPos = originalCameraPosition;
        camPos.y = camera.orthographicSize - originalOrthograficSize;
        camera.transform.position = camPos;
    }
}
