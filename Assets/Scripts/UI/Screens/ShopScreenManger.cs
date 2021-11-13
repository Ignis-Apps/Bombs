using Assets.Scripts.Shop.Tabs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreenManger : AbstractScreenManager
{
    // Init (like Awake) when the script is initialized
    protected override void Init() { }

    // Buttons
    [SerializeField] private Button NextButton;
    [SerializeField] private Button BackButton;
    [SerializeField] private Button HomeButton;

    [SerializeField] private Button BuyButton;

    [SerializeField] private Button SkinTabButton;
    [SerializeField] private Button SceneTabButton;
    [SerializeField] private Button UpgradeTabButton;

    // Wallet
    [SerializeField] private TextMeshProUGUI CyrstalBalanceText;
    [SerializeField] private TextMeshProUGUI CoinBalanceText;

    //Shop items
    [SerializeField] private Skin[] Skins;
    [SerializeField] private Scene[] Scenes;
    [SerializeField] private PowerupUpgrade[] Upgrades;

    private ShopTabInterface currentTab;
    private SkinShopTab skinShopTab;
    private SceneShopTab sceneShopTab;
    private UpgradeShopTab upgradeShopTab;

    private void OnEnable()
    {
        skinShopTab = new SkinShopTab(BuyButton.GetComponent<SBS_Button>(), BackButton, NextButton, Skins);
        sceneShopTab = new SceneShopTab(BuyButton.GetComponent<SBS_Button>(), BackButton, NextButton, Scenes);
        upgradeShopTab = new UpgradeShopTab(BuyButton.GetComponent<SBS_Button>(), BackButton, NextButton, Upgrades);

        UpdateBalance();

        currentTab = skinShopTab;
        currentTab.TabSelected();
    }

    private void Start()
    {
        NextButton.onClick.AddListener(() => currentTab.Next());
        BackButton.onClick.AddListener(() => currentTab.Previous());
        BuyButton.onClick.AddListener(() => { currentTab.Buy(); UpdateBalance(); });
        
        HomeButton.onClick.AddListener(Home);

        SkinTabButton.onClick.AddListener(() => SetCurrentTab(skinShopTab));
        SceneTabButton.onClick.AddListener(() => SetCurrentTab(sceneShopTab));
        UpgradeTabButton.onClick.AddListener(() => SetCurrentTab(upgradeShopTab));
    }

    private void SetCurrentTab(ShopTabInterface shopTabInterface)
    {
        currentTab.TabDeselected();
        currentTab = shopTabInterface;
        currentTab.TabSelected();
    }   

    private void Home()
    {
        gameManager.getPlayer().gameObject.SetActive(true);
        skinShopTab.TabDeselected();
        gameData.SaveData();
        screenManager.SwitchScreen(ScreenType.TITLE_SCREEN);
    }  

    private void UpdateBalance()
    {
        CyrstalBalanceText.text = gameData.CrystalBalance.ToString();
        CoinBalanceText.text = gameData.CoinBalance.ToString();
    }
}