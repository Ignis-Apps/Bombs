using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    public Image image;
    public Camera cam;
    public float zoom;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);

        Debug.Log(zoom);
        Debug.Log(cam.orthographicSize);

        if (cam.orthographicSize > zoom)
        {
            Vector3 camPos = cam.transform.position;
            camPos.y -= 1f;
            cam.transform.position = camPos;
        } else if(cam.orthographicSize < zoom)
        {
            Vector3 camPos = cam.transform.position;
            camPos.y += 1f;
            cam.transform.position = camPos;
        }

        cam.orthographicSize = zoom;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        
            
    }
}
