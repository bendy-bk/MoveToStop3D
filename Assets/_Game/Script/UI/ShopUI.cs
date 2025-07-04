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


    public ListPackCoin Lists => ShopManager.Instance.ListPackCoins;

    private void Awake()
    {
        OnInit();
    }

    private void OnEnable()
    {
        setting.onClick.AddListener(OpenSetting);
    }

    private void OnDisable()
    {
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
            item.Setup(packData);
        }
    }
}
