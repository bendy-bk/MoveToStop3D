
using UnityEngine;
using UnityEngine.UI;

public class PopupSettingUI : UICanvas
{
    [SerializeField] private Button close; 
    [SerializeField] private Button menu;

    [SerializeField] private Slider audioSFX;
    [SerializeField] private Slider music;
    [SerializeField] private GameObject parent;

    private void OnEnable()
    {
        close.onClick.AddListener(ClosePopup);
        menu.onClick.AddListener(BackToMenu);
    }

    private void OnDisable()
    {
        close.onClick.RemoveAllListeners();
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
    public void BackToMenu()
    {
        UIManager.Instance.OpenUI<MainUI>();
        parent.SetActive(false);
        ClosePopup();
    }

}
