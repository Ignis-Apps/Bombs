using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop.Tabs
{
    public class SceneShopTab : AbstractShopTab<Scene>
    {
        public SceneShopTab(SBS_Button buy, Button back, Button next, Scene[] dataset) : base (buy, back, next, dataset) { }

        protected override void OnBuyPressed()
        {
            //throw new NotImplementedException();
        }

        protected override void OnSelectionChanged(Scene item)
        {
            //throw new NotImplementedException();
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
            SBSButton.gameObject.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
            //UpdateNavigationButtons(Scenes, CurrentScene.id);
        }
    }
}
