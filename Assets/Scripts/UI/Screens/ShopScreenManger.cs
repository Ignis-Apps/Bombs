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
    [SerializeField] private Image BuyButtonImage;
    [SerializeField] private Sprite CrystalSprite;
    [SerializeField] private Sprite CoinSprite;
    [SerializeField] private TextMeshProUGUI BuyButtonPrice;
    [SerializeField] private TextMeshProUGUI BuyButtonText;

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

    private Skin CurrentSkin;
    private Scene CurrentScene;
    private PowerupUpgrade CurrentUpgrade;

    private PlayerSkinChanger skinChanger;

    private enum ShopTab
    {
        SKINS,
        SCENES,
        UPGRADES
    }

    private ShopTab currentShopTab;


    private void Start()
    {
        UpdateBalance();

        NextButton.onClick.AddListener(OnNext);
        BackButton.onClick.AddListener(OnBack);
        HomeButton.onClick.AddListener(Home);
        BuyButton.onClick.AddListener(OnBuy);
        SkinTabButton.onClick.AddListener(OnSkinTabSelected);
        SceneTabButton.onClick.AddListener(OnSceneTabSelected);
        UpgradeTabButton.onClick.AddListener(OnUpgradeTabSelected);

        BuyButtonImage = BuyButton.transform.Find("CurrencyImage").GetComponent<Image>();
        BuyButtonPrice = BuyButton.transform.Find("Price").GetComponent<TextMeshProUGUI>();
        BuyButtonText = BuyButton.transform.Find("BuyButtonText").GetComponent<TextMeshProUGUI>();

        skinChanger = gameManager.getPlayer().PlayerSkinChanger;

        CurrentSkin = Skins[gameData.SelectedSkin];
        //CurrentScene = Scenes[gameData.SelectedScene];

        OnSkinTabSelected();
    }

    void OnNext()
    {
        if(currentShopTab == ShopTab.SKINS)
        {
            CurrentSkin = Skins[(CurrentSkin.id + 1) % Skins.Length];
            skinChanger.ApplySkinConfiguration(CurrentSkin.GetFullPlayerSkinConfiguration());

            UpdateNavigationButtons(Skins, CurrentSkin.id);
            UpdateBuyButtonForSkins();
        }
    }

    void OnBack()
    {
        if (currentShopTab == ShopTab.SKINS)
        {
            CurrentSkin = Skins[(CurrentSkin.id - 1) % Skins.Length];
            skinChanger.ApplySkinConfiguration(CurrentSkin.GetFullPlayerSkinConfiguration());

            UpdateNavigationButtons(Skins, CurrentSkin.id);
            UpdateBuyButtonForSkins();
        }
    }

    void OnBuy()
    {
        if (currentShopTab == ShopTab.SKINS)
        {
            if (gameData.GetSkinInventory(CurrentSkin.id))
            {
                if(CurrentSkin.id != gameData.SelectedSkin)
                {
                    BuyButton.enabled = true;
                    gameData.SelectedSkin = CurrentSkin.id;
                    UpdateBuyButtonForSkins();
                }
            }
            else
            {
                if (CurrentSkin.currency == Currency.COINS && CurrentSkin.price <= gameData.CoinBalance)
                {
                    gameData.CoinBalance -= CurrentSkin.price;
                    gameData.SetSkinInventory(CurrentSkin.id, true);
                }
                else if (CurrentSkin.currency == Currency.CRYSTALS && CurrentSkin.price <= gameData.CrystalBalance)
                {
                    gameData.CrystalBalance -= CurrentSkin.price;
                    gameData.SetSkinInventory(CurrentSkin.id, true);
                }
                UpdateBuyButtonForSkins();
                UpdateBalance();
            }
        }
    }

    private void Home()
    {
        Reset();
        screenManager.SwitchScreen(ScreenType.TITLE_SCREEN);
    }

    void OnSkinTabSelected()
    {
        ShowText("Selected");
        currentShopTab = ShopTab.SKINS;

        // Visuals
        gameManager.getPlayer().gameObject.SetActive(true);

        UpdateNavigationButtons(Skins, CurrentSkin.id);
        UpdateBuyButtonForSkins();
    }

    void OnSceneTabSelected()
    {
        ShowText("Coming Soon ...");
        currentShopTab = ShopTab.SCENES;

        // Visuals
        gameManager.getPlayer().gameObject.SetActive(false);

        // Temporary
        BackButton.gameObject.SetActive(false);
        NextButton.gameObject.SetActive(false);
        BuyButton.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);

        //UpdateNavigationButtons(Scenes, CurrentScene.id);
    }

    void OnUpgradeTabSelected()
    {
        ShowText("Coming Soon ...");
        currentShopTab = ShopTab.UPGRADES;

        // Visuals
        gameManager.getPlayer().gameObject.SetActive(false);

        // Temporary
        BackButton.gameObject.SetActive(false);
        NextButton.gameObject.SetActive(false);
        BuyButton.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);

        //UpdateNavigationButtons(Upgrades, CurrentUpgrade.id);
    }

    private void ShowText(string text)
    {
        BuyButtonText.SetText(text);

        BuyButtonPrice.gameObject.SetActive(false);
        BuyButtonImage.gameObject.SetActive(false);
        BuyButtonText.gameObject.SetActive(true);
    }

    private void ShowPrice(int price, Currency currency)
    {
        BuyButtonPrice.SetText(price.ToString());

        if(currency == Currency.COINS)
        {
            BuyButtonImage.sprite = CoinSprite;
        }
        else if(currency == Currency.CRYSTALS)
        {
            BuyButtonImage.sprite = CrystalSprite;
        }

        BuyButtonText.gameObject.SetActive(false);
        BuyButtonPrice.gameObject.SetActive(true);
        BuyButtonImage.gameObject.SetActive(true);
    }

    private void UpdateNavigationButtons(Object[] objects, int currentObject)
    {
        if (currentObject == 0)
        {
            BackButton.gameObject.SetActive(false);
        }
        else
        {
            BackButton.gameObject.SetActive(true);
        }

        if (currentObject == objects.Length - 1)
        {
            NextButton.gameObject.SetActive(false);
        }
        else
        {
            NextButton.gameObject.SetActive(true);
        }
    }

    private void UpdateBuyButtonForSkins()
    {
        if (gameData.GetSkinInventory(CurrentSkin.id))
        {
            BuyButton.enabled = true;
            if (CurrentSkin.id == gameData.SelectedSkin)
            {
                ShowText("Selected");
                BuyButton.GetComponent<Image>().color = new Color(0, 1, 0, 0.3f);
            }
            else
            {
                ShowText("Select");
                BuyButton.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
            }
        }
        else
        {
            ShowPrice(CurrentSkin.price, CurrentSkin.currency);

            BuyButton.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
            if ((CurrentSkin.currency == Currency.COINS && CurrentSkin.price > gameData.CoinBalance) ||
                (CurrentSkin.currency == Currency.CRYSTALS && CurrentSkin.price > gameData.CrystalBalance))
            {
                BuyButton.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 0.4f);
                BuyButton.enabled = false;
            }
            else
            {
                BuyButton.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                BuyButton.enabled = true;
            }
        }
    }

    private void UpdateBalance()
    {
        CyrstalBalanceText.text = gameData.CrystalBalance.ToString();
        CoinBalanceText.text = gameData.CoinBalance.ToString();
    }

    private void Reset()
    {
        // Player
        gameManager.getPlayer().gameObject.SetActive(true);
        skinChanger.ApplySkinConfiguration(Skins[gameData.SelectedSkin].GetFullPlayerSkinConfiguration());
    }
}
