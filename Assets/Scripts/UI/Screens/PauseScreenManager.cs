using UnityEngine;
using UnityEngine.UI;

public class PauseScreenManager : AbstractScreenManager
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button homeButton;

    // Init (like Awake) when the script is initialized
    protected override void Init() {}

    void Start()
    {
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
