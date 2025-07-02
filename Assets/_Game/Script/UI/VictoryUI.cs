using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : UICanvas
{
    [SerializeField] private Button retry;
    [SerializeField] private Button next;

    private void Awake()
    {
        retry.onClick.AddListener(RetryButton);
        next.onClick.AddListener(NextButton);
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
}
