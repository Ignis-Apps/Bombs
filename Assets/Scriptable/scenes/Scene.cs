using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scene")]
public class Scene : ScriptableObject
{
    public int id;
    public string title;

    public int price;
    public Currency currency;
}
