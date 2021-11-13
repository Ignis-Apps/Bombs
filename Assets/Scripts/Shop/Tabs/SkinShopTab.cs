using Assets.Scripts.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop.Tabs
{
    public class SkinShopTab : AbstractShopTab<Skin>
    {
        private PlayerSkinChanger skinChanger;
        private PlayerSkinChanger.PlayerSkinConfiguration lastSelectedSkinConfiguration;

        public SkinShopTab(SBS_Button buy, Button back, Button next, Skin[] dataset) : base (buy, back, next, dataset) {
            skinChanger = GameManager.GetInstance().getPlayer().PlayerSkinChanger;
        }

        protected override void OnBuyPressed()
        {
            Skin currentSkin = GetCurrentItem();
            if (SBSButton.InBuyState)
            {                
                if (MakeTransaction(currentSkin.currency, currentSkin.price))
                {
                    gameData.SetSkinInventory(currentSkin.id, true);

                    Firebase.Analytics.FirebaseAnalytics.LogEvent(
                        Firebase.Analytics.FirebaseAnalytics.EventSpendVirtualCurrency,
                        new Firebase.Analytics.Parameter[] {
                            new Firebase.Analytics.Parameter(
                                Firebase.Analytics.FirebaseAnalytics.ParameterItemName, currentSkin.title),
                            new Firebase.Analytics.Parameter(
                                Firebase.Analytics.FirebaseAnalytics.ParameterValue, currentSkin.price),
                            new Firebase.Analytics.Parameter(
                                Firebase.Analytics.FirebaseAnalytics.ParameterVirtualCurrencyName, GameData.GetCurrencyName(currentSkin.currency)),
                        }
                    );

                    SBSButton.SetSelect();
                }
            }
            else
            {
                gameData.SelectedSkin = currentSkin.id;
                SBSButton.SetSelected();
                lastSelectedSkinConfiguration = currentSkin.GetFullPlayerSkinConfiguration();
            }
       
        }

        protected override void OnSelectionChanged(Skin item)
        {
            // Change player skin
            skinChanger.ApplySkinConfiguration(item.GetFullPlayerSkinConfiguration());
            
            // Skin currently selected
            if (item.id == gameData.SelectedSkin)
            {
                SBSButton.SetSelected();
                return;
            }

            // Skin bought but not selected
            if (gameData.HasSkinInInventory(item.id))
            {
                SBSButton.SetSelect();
                return;
            }

            // Skin not bought
            SBSButton.SetPrice(item.currency, item.price);
                        
        }

        protected override void OnTabClosed()
        {
            skinChanger.ApplySkinConfiguration(lastSelectedSkinConfiguration);
            gameData.PlayerSkinInGame = lastSelectedSkinConfiguration;
        }

        protected override void OnTabOpened()
        {
            lastSelectedSkinConfiguration = GameManager.GetInstance().getPlayer().PlayerSkinChanger.GetPlayerSkinConfiguration();
            GameManager.GetInstance().getPlayer().gameObject.SetActive(true);

            // Initialises skin dictionary
            foreach(Skin s in dataset)
            {
                if (!gameData.HasSkinInInventory(s.id))
                {
                    if(s.id != 0)
                    {
                        gameData.SetSkinInventory(s.id, false);
                    } else
                    {
                        gameData.SetSkinInventory(s.id, true);
                    }
                }
            }

            // Only works cause index and id are equal
            SetIndex(gameData.SelectedSkin);
        }
    }
}
