using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    
    

    private void Awake()
    {
        OnInit();
    }

    private void Update()
    {
        coin.text = BankSystem.Instance.Coin.ToString();
    }

    private void OnEnable()
    {
        setting.onClick.AddListener(OpenSetting);    
    }

    private void OnDisable()
    {
        setting.onClick?.RemoveListener(OpenSetting);
    }

    public void OnInit()
    {
        foreach (var weaponData in Lists.WeaponDatas)
        {
            WeaponShopItemUI item = Instantiate(itemPrefab, contentTransform);
            item.Setup(weaponData); // tạo hàm này trong WeaponShopItemUI để hiển thị tên, ảnh, giá
        }
    }

    public void OpenSetting()
    {
        popupSeting.SetActive(true);
    }

}
