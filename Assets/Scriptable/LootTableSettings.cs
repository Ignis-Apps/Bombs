using UnityEngine;

[CreateAssetMenu(menuName = "Loot Table/Bomb")]
public class LootTableSettings : ScriptableObject
{

    public LootTableItem[] coinDrop;
    public LootTableItem[] scoreOrbDrop;
    public LootTableItem[] specialCoinDrop;

    public int GetRandomCoinAmount()
    {
        return GetRandomAmount(coinDrop);
    }

    public int GetRandomScoreAmount()
    {
        return GetRandomAmount(scoreOrbDrop);
    }

    public int GetRandomSpecialCoinAmount()
    {
        return GetRandomAmount(specialCoinDrop);
    }

    private int GetRandomAmount(LootTableItem[] lootTable)
    {
        if (lootTable.Length == 0)
        {
            return 0;
        }

        return lootTable[GetRandomIndex(lootTable)].amount;
    }

    private int GetRandomIndex(LootTableItem[] lootTable)
    {
        float cumulativeProbability = 0; 
        for(int i=0; i< lootTable.Length; i++)
        {
            cumulativeProbability += lootTable[i].probability;
        }

        float selection = Random.Range(0, cumulativeProbability);        

        for (int i = 0; i < lootTable.Length; i++)
        {           
            if(selection < lootTable[i].probability){ return i; }
            selection -= lootTable[i].probability;
        }
        return 0;
    }

    [System.Serializable]
    public class LootTableItem
    {
        [SerializeField] public int amount;
        [SerializeField] public float probability;
    }

}
