using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class IAPCoinUI : MonoBehaviour
{
    [SerializeField] private Button buy;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI amount;
    [SerializeField] private TextMeshProUGUI price;

    private int depositCoinAmount;

    private void OnEnable()
    {
        buy.onClick.AddListener(BuyItem);
    }

    private void OnDisable()
    {
        buy.onClick.RemoveListener(BuyItem);
    }


    public void BuyItem()
    {
        BankSystem.Instance.DepositCoin(depositCoinAmount);
        GameEvent.OnCoinFly.Invoke(transform);
    }

    internal void Setup(PackCoin packData)
    {
        amount.text = packData.coinAmount.ToString();
        price.text = packData.price;
        image.sprite = packData.image;

        depositCoinAmount = packData.coinAmount;
    }
}

