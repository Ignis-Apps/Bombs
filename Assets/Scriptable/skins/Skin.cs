using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Skin")]
public class Skin : ScriptableObject
{
    public int id;
    public string title;

    public int price;
    public Currency currency;

    public PlayerSkinChanger.PlayerSkinConfiguration GetFullPlayerSkinConfiguration()
    {
        PlayerSkinChanger.PlayerSkinConfiguration psc = new PlayerSkinChanger.PlayerSkinConfiguration();
        psc.SetAll(id);
        Debug.LogWarning("PSC ID: " + id + " (Title: " + title + ")");
        return psc;
    }
}
