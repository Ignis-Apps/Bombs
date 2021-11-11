using Assets.Scripts.Game;
using UnityEngine;
using UnityEngine.UI;

public static class SoundManager 
{
    public enum Sound
    {
        EXPLOSION,
        COIN,
        CRATE,
        LEVEL_COMPLETE,
        PLAYER_HIT,
        PLAYER_TURN,
        POWERUP_RELEASED,
        UI_BUTTON_CLICK,
    }

    private static GameObject oneShotAudioObject;
    private static AudioSource oneShotAudioSource;

    public static void PlaySound(Sound sound)
    {
        if(oneShotAudioObject == null)
        {
            oneShotAudioObject = new GameObject("Sound");
            oneShotAudioSource = oneShotAudioObject.AddComponent<AudioSource>();
        }
        oneShotAudioSource.volume = GameData.GetInstance().VolumeSFX;
        oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
    }

    public static void PlaySound(Sound sound, Vector3 position)
    {

        GameObject soundObject = new GameObject("Sound");
        soundObject.transform.position = position;
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = GameData.GetInstance().VolumeSFX;
        audioSource.maxDistance = 50f;
        audioSource.spatialBlend = 1f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.dopplerLevel = 0f;
        audioSource.Play();
        Object.Destroy(soundObject, audioSource.clip.length);

    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(SoundAssets.SoundAndClip soundAndClip in SoundAssets.Instance.soundAndClips)
        {
            if(soundAndClip.sound == sound)
            {
                return soundAndClip.Clip;
            }
        }
        Debug.LogError("sound " + sound + " not found !");
        return null;
    }
 
}
