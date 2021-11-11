using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    private static SoundAssets _instance;

    public static SoundAssets Instance
    {
        get
        {
            if (_instance == null) _instance = (Instantiate(Resources.Load("SoundAssets") as GameObject)).GetComponent<SoundAssets>();
            return _instance;
        }
    }

    public SoundAndClip[] soundAndClips;

    [System.Serializable]
    public class SoundAndClip
    {
        public SoundManager.Sound sound;
        [SerializeField] private AudioClip[] audioClips;

        public AudioClip Clip { get { return audioClips[Random.Range(0,audioClips.Length)]; } }
    }


}

