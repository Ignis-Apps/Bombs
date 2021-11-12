using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.Game.Session;
using Assets.Scripts.Game;

public class InGameScreenManager: AbstractScreenManager
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI liveText;    
    [SerializeField] TextMeshProUGUI waveText;    

    [SerializeField] Button pauseButton;

    //[SerializeField] GameObject powerupProgressBarFill;
    [SerializeField] ProgressBarController powerupProgressBarController;

    // Init (like Awake) when the script is initialized
    protected override void Init() {}

    public void Start()
    {  
        pauseButton.onClick.AddListener(Pause);
        UpdateUI();
    }

    public void OnEnable()
    {
        GameSessionEventHandler.coinColltedDelegate += UpdateUI;
    }

    public void OnDisable()
    {
        GameSessionEventHandler.coinColltedDelegate -= UpdateUI;
    }

    private void UpdateUI()
    {
        coinText.text   = gameManager.session.playerStats.Coins.ToString();
        scoreText.text  = GameData.GetTimeString(gameManager.SurvivedSecounds);
        liveText.text   = gameManager.session.playerStats.Lifes.ToString();
        waveText.text   = (gameManager.session.progressStats.SurvivedWaves + 1).ToString();


        if (Powerup.CurrentActivePowerup != null)
        {
            powerupProgressBarController.SetProgress(1f - Powerup.CurrentActivePowerup.GetNormalisedProgress());
        }
        else
        {
            powerupProgressBarController.SetProgress(0f);
        }
    }

    public void Update()
    {
        UpdateUI();
    }

    private void Pause()
    {
        screenManager.SwitchScreen(ScreenType.PAUSE_SCREEN);
        gameManager.handleGameEvent(GameEvent.PAUSE_GAME);
    }

}
