using Assets.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SBS_Button : MonoBehaviour
{
    //
    // [ SET/BUY/SELECT] Button Script (Shop)
    //
    
    [SerializeField] private Image CurrencyImage;
    
    [SerializeField] private TextMeshProUGUI PriceText;
    [SerializeField] private TextMeshProUGUI ButtonText;
    
    [SerializeField] private Sprite CrystalSprite, CoinSprite;

    private static Color BUY_BUTTON_BALANCE_BAD = new Color(0.2f, 0.2f, 0.2f, 0.4f);
    private static Color BUY_BUTTON_BALANCE_GOOD = new Color(0.4f, 0.4f, 0.4f, 0.4f);
    
    private static Color SELECT_BUTTON = new Color(0.4f, 0.4f, 0.4f, 0.4f);
    private static Color SELECTED_BUTTON = new Color(0, 1, 0, 0.3f);

    public bool InBuyState { get; private set; }

    public void SetPrice(Currency currency, int amount)
    {
        
        bool balanceGood;
      
        switch (currency)
        {
            case Currency.COINS:
                CurrencyImage.sprite = CoinSprite;
                balanceGood = GameData.GetInstance().CoinBalance >= amount;
                break;

            case Currency.CRYSTALS:
                CurrencyImage.sprite = CrystalSprite;
                balanceGood = GameData.GetInstance().CrystalBalance >= amount;
                break;

            default:
                Debug.LogError("SBS_Button - Currency not implemented !");
                return;
        }

        GetComponent<Image>().color = (balanceGood) ? BUY_BUTTON_BALANCE_GOOD : BUY_BUTTON_BALANCE_BAD;
        GetComponent<Button>().enabled = balanceGood;
        PriceText.SetText("" + amount);


        InBuyState = true;

        ButtonText.gameObject.SetActive(false);
        PriceText.gameObject.SetActive(true);
        CurrencyImage.gameObject.SetActive(true);
    }
   

    public void SetText(string text)
    {
        ButtonText.SetText(text);
        ButtonText.gameObject.SetActive(true);
        PriceText.gameObject.SetActive(false);
        CurrencyImage.gameObject.SetActive(false);
    }

    public void SetSelect()
    {
        SetText("Select");
        GetComponent<Image>().color = SELECT_BUTTON;
        GetComponent<Button>().enabled = true;
        InBuyState = false;
    }

    public void SetSelected()
    {
        SetText("Selected");
        GetComponent<Image>().color = SELECTED_BUTTON;
        GetComponent<Button>().enabled = true;
        InBuyState = false;
    }

}
