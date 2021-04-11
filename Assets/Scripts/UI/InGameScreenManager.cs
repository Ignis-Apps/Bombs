using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameScreenManager: AbstractScreenManager
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI liveText;    

    [SerializeField] Button pauseButton;

    // Init (like Awake) when the script is initialized
    protected override void Init() {}

    public void Start()
    {
        pauseButton.onClick.AddListener(Pause);
    }

    // TODO : Stop wasting ressources. Maybe we could use event based updates
    void Update()
    {
        coinText.text = gameManager.playerStats.Coins.ToString();
        scoreText.text = gameManager.SurvivedSecounds.ToString();
        liveText.text = gameManager.playerStats.Lifes.ToString();
    }

    private void Pause()
    {
        screenManager.SwitchScreen(ScreenType.PAUSE_SCREEN);
        gameManager.handleGameEvent(GameEvent.PAUSE_GAME);
    }

    
}
