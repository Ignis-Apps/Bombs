using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviveScreenManager : MonoBehaviour
{    
    // Buttons
    [SerializeField] private Button reviveWithCrystalButton;
    [SerializeField] private Button reviveWithAdButton;

    // Visuals 
    [SerializeField] private Transform progressBarFill;
    [SerializeField] private TextMeshProUGUI progressText;

    private GameManager gameManager;
    private ScreenManager screenManager;

    private void Awake()
    {
        gameManager = GameManager.GetInstance();
        screenManager = ScreenManager.GetInstance();
        reviveWithCrystalButton.onClick.AddListener(OnReviveWithCrystal);
        reviveWithAdButton.onClick.AddListener(OnReviveWithAd);
    }

    private void OnEnable()
    {
        float progress = gameManager.CurrentWaveProgress;
        progressBarFill.localScale = new Vector3 (progress,1f,1f);
        progressText.text = ((int)(progress * 100)) + "%";
    }

    private void OnReviveWithCrystal() {
        Revive();
    }
    private void OnReviveWithAd() {
        Revive();
    }

    private void Revive()
    {
        gameManager.handleGameEvent(GameEvent.REVIVE_PLAYER);
        screenManager.SwitchScreen(ScreenType.INGAME_OVERLAY);

    }
}
