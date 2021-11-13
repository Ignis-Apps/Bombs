using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    private List<TabButton> tabButtons;
    public TabButton selectedTab;

    public Color idleColor;
    public Color hoverColor;
    public Color activeColor;
    
    private void Awake()
    {
        tabButtons = GetComponentsInChildren<TabButton>(true).ToList();                
    }
 
    public void OnTabSelected(TabButton button)
    {
        tabButtons.ForEach(tabButtons => tabButtons.OnDeselected());
        selectedTab = button;
        button.OnSelected();  
    }

    // Start is called before the first frame update
    void Start()
    {
        tabButtons.ForEach(tabButtons => { 
            tabButtons.OnDeselected();  
            tabButtons.GetComponentInParent<Button>().onClick.AddListener(() => OnTabSelected(tabButtons)); 
        });
        selectedTab.OnSelected();

    }

    private void OnEnable()
    {
        if(selectedTab != null)
            selectedTab.OnSelected();
    }

    private void OnDisable()
    {
        CameraManager camManager = Camera.main.GetComponent<CameraManager>();
        camManager.MoveToView(camManager.originalCameraPosition, camManager.originalOrthograficSize, .3f);
    }
    
}
