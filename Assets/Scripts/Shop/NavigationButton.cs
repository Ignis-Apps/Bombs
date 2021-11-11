using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NavigationButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Image image;

    public Color idleColor;
    public Color hoverColor;

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = idleColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        idleColor = image.color;
    }
}
