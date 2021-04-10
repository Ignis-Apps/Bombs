using UnityEngine;
using UnityEngine.UI;

public class PauseScreenManager : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button homeButton;

    private ScreenManager screenManager;
    private GameManager gameManager;

    void Start()
    {
        screenManager = ScreenManager.GetInstance();
        gameManager = GameManager.GetInstance();

        resumeButton.onClick.AddListener(ResumeGame);
        homeButton.onClick.AddListener(Home);
    }

    private void ResumeGame()
    {
        screenManager.SwitchScreen(ScreenType.INGAME_OVERLAY);
        gameManager.handleGameEvent(GameEvent.RESUME_GAME);
    }

    private void Home()
    {
        screenManager.SwitchScreen(ScreenType.TITLE_SCREEN);
        gameManager.handleGameEvent(GameEvent.RESET_GAME);
    }
}
