using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : UICanvas
{
    [SerializeField] private Button retry;
    [SerializeField] private Button next;
    [SerializeField] private Button settingbtn;
    [SerializeField] private GameObject settingPopup;

    private void Awake()
    {
        retry.onClick.AddListener(RetryButton);
        next.onClick.AddListener(NextButton);
        settingbtn.onClick.AddListener(PopUpSetting);
    }

    public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        Close();
    }

    public void NextButton()
    {
        LevelManager.Instance.OnNextLevel();
        Close();
    }
    public void PopUpSetting()
    {
        settingPopup.SetActive(true);
    }
}
