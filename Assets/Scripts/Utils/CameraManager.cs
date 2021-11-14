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
    public float originalOrthograficSize { get; private set; }
    public Vector3 originalCameraPosition { get; private set; }

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

    public void ResetCameraPosition()
    {
        fitCameraToScreen();
    }

    private IEnumerator CameraMovementAnimation;

    public void MoveToView(Vector3 position, float orthographicSize, float time)
    {

        if (CameraMovementAnimation != null)
            StopCoroutine(CameraMovementAnimation);
        
        CameraMovementAnimation = AnimateCameraMovement(position, orthographicSize, time);
        StartCoroutine(CameraMovementAnimation);
        
    }

    IEnumerator AnimateCameraMovement(Vector3 targetPosition, float orthographicSize, float time)
    {
        Vector3 startPosition = cam.transform.position;

        Debug.Log(targetPosition);

        float startSize = cam.orthographicSize;
        float elapsedTime = 0f;
        
        while(elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            cam.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime/time);
            cam.orthographicSize = Mathf.Lerp(startSize, orthographicSize, elapsedTime/time);
            yield return null;
        }
                       
    }
}
