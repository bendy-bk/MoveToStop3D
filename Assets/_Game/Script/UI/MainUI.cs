using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : UICanvas
{
    [SerializeField] private Button play;


    private void Awake()
    {
        play.onClick.AddListener(PlayButton);
    }

    public void PlayButton()
    {
        LevelManager.Instance.OnStartGame();
        //UIManager.Instance.OpenUI<Gameplay>();
        Close();
    }


}
