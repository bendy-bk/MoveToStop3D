using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : UICanvas
{
    [SerializeField] private Button play;
    [SerializeField] private Button Shop;
    [SerializeField] private Button Weapon;
    [SerializeField] private Button LuckySpin;
    [SerializeField] private Button Setting;

    [SerializeField] private TextMeshProUGUI coin;

    private void Start()
    {
        coin.text = BankSystem.Instance.Coin.ToString();
    }

    private void OnEnable()
    {
        coin.text = BankSystem.Instance.Coin.ToString();
        play.onClick.AddListener(PlayButton);
        Shop.onClick.AddListener(OpenShop);
        Weapon.onClick.AddListener(OpenWeapon);
        LuckySpin.onClick.AddListener(OpenLuckySpin);
        Setting.onClick.AddListener(OpenSetting);
    }
    private void OnDisable()
    {
        play.onClick.RemoveListener(PlayButton);
        Shop.onClick.RemoveListener(OpenShop);
        Weapon.onClick.RemoveListener(OpenWeapon);
        LuckySpin.onClick.RemoveListener(OpenLuckySpin);
        Setting.onClick.RemoveListener(OpenSetting);
    }


    public void PlayButton()
    {
        LevelManager.Instance.OnStartGame();
        //UIManager.Instance.OpenUI<Gameplay>();
        Close();
    }

    public void OpenShop()
    {
        UIManager.Instance.OpenUI<ShopUI>();
        UIManager.Instance.CloseUI<MainUI>();
    }
    public void OpenWeapon()
    {
        UIManager.Instance.OpenUI<WeaponUI>();
        UIManager.Instance.CloseUI<MainUI>();

    }
    public void OpenLuckySpin()
    {

    }

    public void OpenSetting()
    {

    }


}
