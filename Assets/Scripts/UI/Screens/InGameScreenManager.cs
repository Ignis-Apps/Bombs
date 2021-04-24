using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    }

    // TODO : Stop wasting ressources. Maybe we could use event based updates
    void Update()
    {
        coinText.text = gameManager.playerStats.Coins.ToString();
        scoreText.text = CreateTimeString(gameManager.SurvivedSecounds);
        liveText.text = gameManager.playerStats.Lifes.ToString();
        waveText.text = (gameManager.SurvivedWaves + 1).ToString(); 

        
        if(Powerup.CurrentActivePowerup != null)
        {
            powerupProgressBarController.SetProgress(1f - Powerup.CurrentActivePowerup.GetNormalisedProgress());
        }
        else
        {
            powerupProgressBarController.SetProgress(0f);
        }
        
        

    }
    private void Pause()
    {
        screenManager.SwitchScreen(ScreenType.PAUSE_SCREEN);
        gameManager.handleGameEvent(GameEvent.PAUSE_GAME);
    }

    private string CreateTimeString(int seconds)
    {
        string output;
        int min = seconds / 60;
        int sec = seconds % 60;

        output = min + ":";
        if (sec < 10) { output += "0"; }
        output += sec;

        return output;
    }

}
