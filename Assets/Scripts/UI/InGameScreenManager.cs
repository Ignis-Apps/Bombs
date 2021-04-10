using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameScreenManager: MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI liveText;

    [SerializeField] Button pauseButton;

    private ScreenManager screenManager;
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
        screenManager = ScreenManager.GetInstance();

        pauseButton.onClick.AddListener(Pause);
    }
    // TODO : Stop wasting ressources. Maybe we could use event based updates
    void Update()
    {
        coinText.text = gameManager.playerStats.Coins.ToString();
        scoreText.text = gameManager.playerStats.Score.ToString();
        liveText.text = gameManager.playerStats.Lifes.ToString();
    }

    private void Pause()
    {
        screenManager.SwitchScreen(ScreenType.PAUSE_SCREEN);
        gameManager.handleGameEvent(GameEvent.PAUSE_GAME);
    }
}
