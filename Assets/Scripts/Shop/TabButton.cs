using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    public Image image;
    public float relativOrthograficCameraSizeChange;
    public float relativYCameraMovment;

    private bool IsSelected;

    public void Awake()
    {
        image = GetComponent<Image>();        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      
        image.color = tabGroup.hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsSelected)
            image.color = tabGroup.activeColor;
        else
            image.color = tabGroup.idleColor;
    }

    public void OnSelected()
    {
        IsSelected = true;
        image.color = tabGroup.activeColor;
        SetRelativeCameraPosition();
    }
    public void OnDeselected()
    {
        IsSelected = false;
        image.color = tabGroup.idleColor;        
    }

    private void SetRelativeCameraPosition()
    {
        CameraManager camManager = Camera.main.GetComponent<CameraManager>();
        Vector3 targetPosition = camManager.originalCameraPosition;
        float targetSize = camManager.originalOrthograficSize;
      
        targetPosition.y += relativOrthograficCameraSizeChange;
        targetSize += relativOrthograficCameraSizeChange;
          
        camManager.MoveToView(targetPosition, targetSize, .5f);
    }

   
    
    
}
