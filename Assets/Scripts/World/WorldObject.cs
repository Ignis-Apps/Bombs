using UnityEngine;

namespace Assets.Scripts.World
{
    [CreateAssetMenu(menuName = "World")]
    public class WorldObject : ScriptableObject
    {

        [SerializeField] private Gradient[] SKY_GRADIENTS;

        public Gradient GetSkyGradient(float index)
        {

            return SKY_GRADIENTS[(int)nfmod(Mathf.FloorToInt(index), SKY_GRADIENTS.Length)];
        }

        float nfmod(float a, float b)
        {
            return a - b * Mathf.Floor(a / b);
        }


    }
}
