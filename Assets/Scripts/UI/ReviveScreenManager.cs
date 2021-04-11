using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviveScreenManager : AbstractScreenManager
{
    // Configuration
    [SerializeField] private float timeToDecide;

    // Buttons
    [SerializeField] private Button reviveWithCrystalButton;
    [SerializeField] private Button reviveWithAdButton;

    // Visuals 
    [SerializeField] private Transform progressBarFill;
    [SerializeField] private Transform timeBarFill;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI waveText;

    private bool timeBarRunning;

    // Init (like Awake) when the script is initialized
    protected override void Init() {}

    private void Start()
    {
        reviveWithCrystalButton.onClick.AddListener(OnReviveWithCrystal);
        reviveWithAdButton.onClick.AddListener(OnReviveWithAd);
    }

    private void OnEnable()
    {
        float progress = gameManager.CurrentWaveProgress;
        progressBarFill.localScale = new Vector3 (progress,1f,1f);
        timeBarFill.localScale = new Vector3(1f, timeBarFill.localScale.y, timeBarFill.localScale.z);
        timeBarRunning = true;
        progressText.text = ((int)(progress * 100)) + "%";        
        waveText.text = "( Wave " + (gameManager.SurvivedWaves + 1) + " )";
    }

    private void Update()
    {
        if (timeBarRunning)
        {
            Vector3 currentScale = timeBarFill.localScale;
            currentScale.x = Mathf.Max(0f, currentScale.x - (Time.unscaledDeltaTime / timeToDecide));
            timeBarFill.localScale = currentScale;
            
            if (currentScale.x <= 0)
            {
                Die();
            }
        }
    }

    private void OnReviveWithCrystal() {
        timeBarRunning = false;
        Revive();
    }
    private void OnReviveWithAd() {
        timeBarRunning = false;
        Revive();
    }

    private void Revive()
    {
        gameManager.handleGameEvent(GameEvent.REVIVE_PLAYER);
        screenManager.SwitchScreen(ScreenType.INGAME_OVERLAY);
    }

    private void Die()
    {
        screenManager.SwitchScreen(ScreenType.GAME_OVER_SCREEN);
    }
}
