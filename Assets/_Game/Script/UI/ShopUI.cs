using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UICanvas
{
    [SerializeField] private Transform contentTransform; // kéo Content của ScrollView vào đây trong Inspector
    [SerializeField] private IAPCoinUI itemPrefab; // prefab UI mỗi item
    [SerializeField] TextMeshProUGUI coin;
    [SerializeField] Button setting;
    [SerializeField] private GameObject popupSeting;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform canvasTransform;
    public ListPackCoin Lists => ShopManager.Instance.ListPackCoins;
    public List<IAPCoinUI> iAPCoinUIs = new List<IAPCoinUI>();
    

    private void Awake()
    {
        OnInit();
    }

    private void OnEnable()
    {
        GameEvent.OnCoinFly.AddListener(CoinFly);
        setting.onClick.AddListener(OpenSetting);
    }



    private void OnDisable()
    {
        GameEvent.OnCoinFly.RemoveListener(CoinFly);
        setting.onClick?.RemoveListener(OpenSetting);
    }

    private void Update()
    {
        coin.text = BankSystem.Instance.Coin.ToString();
    }

    public void OpenSetting()
    {
        popupSeting.SetActive(true);
    }

    public void OnInit()
    {
        foreach (var packData in Lists.ListPacks)
        {
            IAPCoinUI item = Instantiate(itemPrefab, contentTransform);
            iAPCoinUIs.Add(item);
            item.Setup(packData);
        }
    }

    private void CoinFly(Transform t)
    {
        StartCoroutine(SpawnCoins(t, transform));
    }

   

    IEnumerator SpawnCoins(Transform startPoint, Transform targetPoint)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject coin = Instantiate(coinPrefab, canvasTransform);
            coin.transform.position = startPoint.position;
            Debug.Log(coin);
            // Fly to target
            coin.transform.DOMove(targetPoint.position, 0.6f)
                .SetEase(Ease.InBack)
                .OnComplete(() => Destroy(coin)); // Xoá coin sau khi bay xong

            yield return new WaitForSeconds(0.05f); // Delay giữa mỗi đồng
        }
    }
}
