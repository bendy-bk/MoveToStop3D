using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopItemUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameWeapon;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI equipTxt;

    private List<WeaponDTO> weaponowner => EquipmentManager.Instance.WeaponDTOs;
    private WeaponData data;

    [SerializeField] Button buy;
    [SerializeField] Button equip;

    private void OnEnable()
    {
        equip.onClick.AddListener(Equip);
        buy.onClick.AddListener(BuyWeapon);
    }

    private void OnDisable()
    {
        buy?.onClick.RemoveListener(BuyWeapon);
        equip.onClick.RemoveListener(Equip);
    }

    public void Setup(WeaponData weaponData)
    {
        data = weaponData;
        nameWeapon.text = weaponData.weaponName;
        image.sprite = weaponData.icon;

        bool isOwned = false;
        bool isEquipped = false;

        if (weaponowner != null)
        {
            foreach (WeaponDTO weapon in weaponowner)
            {
                if (weapon.weaponType == weaponData.weaponType)
                {
                    isOwned = true;
                    isEquipped = weapon.isEquipped; // kiểm tra trạng thái equipped
                    break;
                }
            }
        }

        if (isOwned)
        {
            equip.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
            equipTxt.text = isEquipped ? "Equipped" : "Equip"; // cập nhật text
        }
        else
        {
            buy.gameObject.SetActive(true);
            equip.gameObject.SetActive(false);
            price.text = weaponData.price.ToString();
        }
    }


    public void BuyWeapon()
    {
        if (BankSystem.Instance.WithdrawCoin(data.price))
        {
            EquipmentManager.Instance.UnlockWeapon(data.weaponType);
            Setup(data);
        }
        else
        {
            Debug.LogError("Not enough coin");
        }


    }

    public void Equip()
    {
        EquipmentManager.Instance.EquipWeapon(data.weaponType);
        Setup(data);
    }


}
