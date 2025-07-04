
using System;
using UnityEngine;

public class ShopManager : GenericSingleton<ShopManager>
{
    [SerializeField] private ListPackCoin listPackCoins;

    public ListPackCoin ListPackCoins { get => listPackCoins; set => listPackCoins = value; }
}
