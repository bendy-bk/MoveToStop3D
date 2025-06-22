
using System;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : GenericSingleton<LevelManager>
{
    public Level[] levelPrefabs;
    public Bot botPrefab;
    public Player player;


    private int levelIndex;
    private Level currentLevel;

    public int CharacterAmount => currentLevel.botAmout + 1;

    private void Awake()
    {
        PlayerPrefs.SetInt("Level", 0);
        //levelIndex = PlayerPrefs.GetInt("Level", 0);
    }

    public void Start()
    {   
        UIManager.Instance.OpenUI<MainUI>();
    }

    public void OnInit()
    {
        //Update navmeshData
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(currentLevel.navMeshData);

        //SetUp player
        player.TF.position = currentLevel.startPoint.position;
        player.TF.rotation = Quaternion.identity;

        //Khoi tao player
        player.OnInit();

        BotManger.Instance.CurLevel = currentLevel;
        BotManger.Instance.SpawnBot(CharacterAmount);

        UIManager.Instance.OpenUI<JoystickControl>();
    }

    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        if (level < levelPrefabs.Length)
        {
            currentLevel = Instantiate(levelPrefabs[level]);
            //currentLevel.OnInit();
        }
        else
        {
            // Done game
        }

    }

    public void OnReset()
    {
        player.totalKill = 0;
    }

    public void OnRetry()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
        UIManager.Instance.CloseUI<JoystickControl>();
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
    }

    public void OnNextLevel()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
        UIManager.Instance.CloseUI<JoystickControl>();
        levelIndex++;
        PlayerPrefs.SetInt("Level", levelIndex);
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
    }

    internal void CheckWin(int totalKill)
    {
        if (totalKill >= currentLevel.targetKillBot)
        {
            GameManager.Instance.ChangeState(GameState.Pause);
            UIManager.Instance.CloseUI<JoystickControl>();
            UIManager.Instance.OpenUI<VictoryUI>();
            
        }
    }

    internal void OnStartGame()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
        LoadLevel(levelIndex);
        OnInit();
    }

    internal void Lose()
    {

        GameManager.Instance.ChangeState(GameState.Pause);
        UIManager.Instance.OpenUI<LoseUI>();
        UIManager.Instance.CloseUI<JoystickControl>();
    }
}

