using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    public Image image;
    public float relativOrthograficCameraSizeChange;
    public float relativYCameraMovment;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    public void OnSelected()
    {
        tabGroup.orthograficCameraSize += relativOrthograficCameraSizeChange;
        tabGroup.cameraYPosition += + relativYCameraMovment;
    }
    public void OnDeselected()
    {
        tabGroup.orthograficCameraSize -= relativOrthograficCameraSizeChange;
        tabGroup.cameraYPosition -= relativYCameraMovment;
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }
}
