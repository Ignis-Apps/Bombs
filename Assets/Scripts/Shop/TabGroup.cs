using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public TabButton selectedTab;

    public Color idleColor;
    public Color hoverColor;
    public Color activeColor;

    public float orthograficCameraSize;
    public float cameraYPosition;
    public Camera cam;


    public void Subscribe(TabButton tabButton)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(tabButton);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        {
            button.image.color = hoverColor;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        if(selectedTab != null && selectedTab != button)
        {
            selectedTab.OnDeselected();
        }
        if(selectedTab != button)
        {
            button.OnSelected();
        }
        selectedTab = button;
        ResetTabs();
        button.image.color = activeColor;
    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if(selectedTab != null && selectedTab == button) { continue; }
            button.image.color = idleColor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        orthograficCameraSize = cam.orthographicSize;
        cameraYPosition = cam.transform.position.y;

        ResetTabs();
        selectedTab.OnSelected();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(cam.orthographicSize - orthograficCameraSize) < 0.005f)
        {
            cam.orthographicSize = orthograficCameraSize;
        }
        else
        {
            cam.orthographicSize += (orthograficCameraSize - cam.orthographicSize) / 40;
        }

        if (Mathf.Abs(cam.transform.position.y - cameraYPosition) < 0.005f)
        {
            Vector3 camPos = cam.transform.position;
            camPos.y = cameraYPosition;
            cam.transform.position = camPos;
        }
        else
        {
            Vector3 camPos = cam.transform.position;
            camPos.y += (cameraYPosition - camPos.y) / 40;
            cam.transform.position = camPos;
        }
    }

    private void OnDisable()
    {
        cam.GetComponent<CameraManager>().resetCameraPosition();
    }
}
