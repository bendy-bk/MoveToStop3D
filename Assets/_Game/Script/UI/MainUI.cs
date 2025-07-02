using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : UICanvas
{
    [SerializeField] private Button play;
    [SerializeField] private Button IAP;
    [SerializeField] private TextMeshProUGUI coin;

    private void Start()
    {
        coin.text = BankSystem.Instance.Coin.ToString();
    }

    private void OnEnable()
    {
        coin.text = BankSystem.Instance.Coin.ToString();
        play.onClick.AddListener(PlayButton);
        IAP.onClick.AddListener(IAPcoin);
    }
    private void OnDisable()
    {
        play.onClick.RemoveListener(PlayButton);
        IAP.onClick.RemoveListener(IAPcoin);
    }


    public void PlayButton()
    {
        LevelManager.Instance.OnStartGame();
        //UIManager.Instance.OpenUI<Gameplay>();
        Close();
    }

    public void IAPcoin()
    {
        BankSystem.Instance.DepositCoin(22);
    }


}
