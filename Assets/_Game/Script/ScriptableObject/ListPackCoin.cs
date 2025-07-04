using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coin", menuName = "SO/Coin")]
[Serializable]
public class ListPackCoin : ScriptableObject
{
    [SerializeField] private List<PackCoin> listPacks = new List<PackCoin>();

    public List<PackCoin> ListPacks { get => listPacks; set => listPacks = value; }
}


[Serializable]
public class PackCoin
{
    public int coinAmount;
    public string price;
    public Sprite image;
}
