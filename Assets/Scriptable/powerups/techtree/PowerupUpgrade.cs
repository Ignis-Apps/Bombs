using Assets.Scripts.Game;
using UnityEngine;

public abstract class PowerupUpgrade : ScriptableObject
{

    [SerializeField] private int UPGRADE_ID;

    public int UpgradeID { get => UPGRADE_ID; }

    public int GetLevel()
    {
        return GameData.GetInstance().getUpgradeInventory(UPGRADE_ID);
    }

    public bool IsUpgradeMaxed()
    {
        return ( GetLevel() + 1 == GetAmountOfUpgrades() );
    }

    
    public abstract int GetAmountOfUpgrades();



}
