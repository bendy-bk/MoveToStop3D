using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : UICanvas
{
    [SerializeField] private Transform contentTransform; // kéo Content của ScrollView vào đây trong Inspector
    [SerializeField] private WeaponShopItemUI itemPrefab; // prefab UI mỗi item
    [SerializeField] private Button setting; // prefab UI mỗi item
    [SerializeField] private GameObject popupSeting;
    [SerializeField] private TextMeshProUGUI coin;

    private ListWeaponSO Lists => EquipmentManager.Instance.ListWeaponSO;
    private List<WeaponShopItemUI> weaponShopItemUIs = new();


    private void Awake()
    {
        OnInit();
        SetupListItem();
    }

    private void Update()
    {
        coin.text = BankSystem.Instance.Coin.ToString();
    }

    private void OnEnable()
    {
        GameEvent.OnResetListWeapon.AddListener(SetupListItem);
        setting.onClick.AddListener(OpenSetting);
    }

    private void OnDisable()
    {
        GameEvent.OnResetListWeapon.RemoveListener(SetupListItem);
        setting.onClick?.RemoveListener(OpenSetting);
    }

    public void OnInit()
    {
        foreach (var weaponData in Lists.WeaponDatas)
        {
            WeaponShopItemUI item = Instantiate(itemPrefab, contentTransform);
            weaponShopItemUIs.Add(item);     
        }
    }

    public void SetupListItem()
    {
        for (int i = 0; i < weaponShopItemUIs.Count; i++)
        {
            weaponShopItemUIs[i].Setup(Lists.WeaponDatas[i]);
        }
    }

    public void OpenSetting()
    {
        popupSeting.SetActive(true);
    }


}
