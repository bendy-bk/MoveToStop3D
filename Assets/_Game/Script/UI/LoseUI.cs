using UnityEngine;
using UnityEngine.UI;

public class LoseUI : UICanvas
{
    [SerializeField] private Button retry;
    [SerializeField] private Button settingbtn;
    [SerializeField] private GameObject settingPopup;

    private void Awake()
    {
        retry.onClick.AddListener(RetryButton);
        settingbtn.onClick.AddListener(PopUpSetting);
    }


    public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        Close();
    }
    public void PopUpSetting()
    {
        settingPopup.SetActive(true);
    }
}
