using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject skinsContainer;
    public GameObject scenesContainer;
    public GameObject upgradesContainer;

    private Skin[] skins;
    private Scene[] scenes;
    private Upgrade[] upgrades;

    // Start is called before the first frame update
    void Start()
    {
        skins = skinsContainer.GetComponentsInChildren<Skin>();
        scenes = scenesContainer.GetComponentsInChildren<Scene>();
        upgrades = upgradesContainer.GetComponentsInChildren<Upgrade>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
