using Assets.Scripts.Game;
using UnityEngine.UI;

namespace Assets.Scripts.Shop.Tabs
{
    public abstract class AbstractShopTab <T> : ShopTabInterface
    {
        protected SBS_Button SBSButton;
        protected Button BackButton;
        protected Button NextButton;

        public T[] dataset;
        private int datasetIndex;

        protected GameData gameData;

        public AbstractShopTab(SBS_Button buy, Button back, Button next, T[] data)
        {            
            SBSButton = buy;
            BackButton = back;
            NextButton = next;
            gameData = GameData.GetInstance();
            dataset = data;         
        }

        public void Next()
        {
            if (datasetIndex < dataset.Length - 1)
                datasetIndex++;
            
            UpdateNavigationButtons();
            OnSelectionChanged(GetCurrentItem());
        }

        public void Previous()
        {
            if (datasetIndex > 0)
                datasetIndex--;

            UpdateNavigationButtons();
            OnSelectionChanged(GetCurrentItem());
        }

        private void UpdateNavigationButtons()
        {
            BackButton.gameObject.SetActive(datasetIndex > 0);
            NextButton.gameObject.SetActive(datasetIndex < dataset.Length - 1);
        }

        public void Buy()
        {
            OnBuyPressed();
        }

        protected abstract void OnBuyPressed();

        public void TabSelected()
        {            
            OnTabOpened();
            NotifyDataSetChanged();
            UpdateNavigationButtons();
        }

        public void TabDeselected()
        {
            OnTabClosed();
        }

        protected abstract void OnTabOpened();
        protected abstract void OnTabClosed();
        protected abstract void OnSelectionChanged(T item);

        public T GetCurrentItem()
        {
            return dataset[datasetIndex];          
        }

        public bool MakeTransaction(Currency currency, int amount)
        {
            if(currency == Currency.COINS && gameData.CoinBalance >= amount)
            {
                gameData.CoinBalance -= amount;
                return true;
            }else if (currency == Currency.CRYSTALS && gameData.CrystalBalance >= amount)
            {
                gameData.CrystalBalance -= amount;
                return true;
            }    

            return false;
        }

        public void NotifyDataSetChanged()
        {
            if (dataset == null)
                return;

            OnSelectionChanged(GetCurrentItem());
        }

        public void SetIndex(int index)
        {
            if(index > 0 && index < dataset.Length - 1)
            {
                datasetIndex = index;
                UpdateNavigationButtons();
                NotifyDataSetChanged();
            }
        }
    }
}
