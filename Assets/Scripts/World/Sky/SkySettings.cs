using UnityEngine;

namespace Assets.Scripts.World.Sky
{
    [CreateAssetMenu(menuName = "World/Sky_Gradient")]
    public class SkySettings : ScriptableObject
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
