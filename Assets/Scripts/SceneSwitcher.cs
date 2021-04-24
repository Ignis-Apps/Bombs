using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private LoadSceneMode mode;
    private bool loaded;

    public void loadScene()
    {
        if (loaded) return;
        
        SceneManager.LoadScene(sceneName, mode);
        loaded = true;
    }

    public void unloadScene()
    {

    }
}
