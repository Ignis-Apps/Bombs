using UnityEngine;

namespace Assets.Scriptable
{
    [CreateAssetMenu(menuName = "Loot Table/Crate")]
    class CrateSettings : ScriptableObject
    {
        [SerializeField] private CrateDrop[] crateDrops;

        public GameObject GetCrateDrop()
        {
            int sumOfWeights = 0;
            foreach (CrateDrop c in crateDrops) { sumOfWeights += c.getWeight(); }
                        
            int index = 0;
            for (int i = Random.Range(0, sumOfWeights); index < crateDrops.Length-1; ++index)
            {
                i -= crateDrops[index].getWeight();
                if (i <= 0) { break; }          
            }

            return crateDrops[index].GetGameObject(); 
        }
    }
    
    [System.Serializable]
    public class CrateDrop
    {
        [SerializeField] private GameObject dropedObjectPrefab;
        [SerializeField] private int weight;

        public int getWeight()
        {
            return weight;
        }

        public GameObject GetGameObject()
        {
            return dropedObjectPrefab;
        }
    }
}
