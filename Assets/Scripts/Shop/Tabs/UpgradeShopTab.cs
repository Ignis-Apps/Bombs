using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop.Tabs
{
    public class UpgradeShopTab : AbstractShopTab<PowerupUpgrade>
    {

        public UpgradeShopTab(SBS_Button buy, Button back, Button next, PowerupUpgrade[] dataset) : base(buy, back, next, dataset) { }

        protected override void OnBuyPressed()
        {
           
        }

        protected override void OnSelectionChanged(PowerupUpgrade item)
        {
           
        }

        protected override void OnTabClosed()
        {
       
        }

        protected override void OnTabOpened()
        {
            SBSButton.SetText("Coming Soon ...");

            // Visuals
            GameManager.GetInstance().getPlayer().gameObject.SetActive(false);

            // Temporary
            BackButton.gameObject.SetActive(false);
            NextButton.gameObject.SetActive(false);
            SBSButton.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);

            //UpdateNavigationButtons(Upgrades, CurrentUpgrade.id);
        }
    }
}
