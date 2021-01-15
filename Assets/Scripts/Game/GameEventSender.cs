using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameEventSender : MonoBehaviour
{
    [SerializeField]
    private GameEvent targetEvent;
    private GameManager gameManager;
  
    void Start()
    {
        gameManager = GameManager.GetInstance();
        GetComponent<Button>().onClick.AddListener(OnButtonPressed);
    }

    void OnButtonPressed()
    {
        gameManager.handleGameEvent(targetEvent);
    }
}
