using UnityEngine;
using UnityEngine.UI;

public class LoseUI : UICanvas
{
    [SerializeField] private Button retry;
  

    private void Awake()
    {
        retry.onClick.AddListener(RetryButton);
    }


    public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        Close();
    }

}
