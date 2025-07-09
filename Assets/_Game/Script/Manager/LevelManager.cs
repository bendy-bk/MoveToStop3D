using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : GenericSingleton<LevelManager>
{
    [SerializeField] private Level[] levelPrefabs;
    [SerializeField] private Bot botPrefab;
    [SerializeField] private Player player;
    [SerializeField] private Transform CanvasGameplay;
    [SerializeField] public List<Character> totalCharacters = new();

    private int levelIndex;
    private Level currentLevel;

    public int CharacterAmount => currentLevel.botAmout + 1;

    private void Awake()
    {
        levelIndex = PlayerPrefs.GetInt(Constants.PLAYERPREF_LEVEL, 0);
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
        UIManager.Instance.OpenUI<JoystickControl>();

        //Khoi tao bot
        BotManger.Instance.CurLevel = currentLevel;
        BotManger.Instance.SpawnBot(CharacterAmount);
       
        //Add player & bot vao danh sach quan li
        InitCharacter();
    }

    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        if (level < levelPrefabs.Length)
        {
            currentLevel = Instantiate(levelPrefabs[level], CanvasGameplay);
        }
        else
        {

            ///TODO
            // Done game
        }

    }

    public void OnReset()
    {
        SimplePool.ReleaseAll();
        player.ResetCharacter();
        totalCharacters.Clear();
        BotManger.Instance.ResetBotManager(); 
    }

    internal void OnStartGame()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
        LoadLevel(levelIndex);
        OnInit();
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
        PlayerPrefs.SetInt(Constants.PLAYERPREF_LEVEL, levelIndex);
        PlayerPrefs.Save();

        OnReset();
        LoadLevel(levelIndex);
        OnInit();
    }

    internal void CheckWin(int totalKill)
    {
        if (player.TotalKill >= currentLevel.targetKillBot)
        {
            GameManager.Instance.ChangeState(GameState.Pause);
            BotManger.Instance.ChangeStateBotNull();
            UIManager.Instance.CloseUI<JoystickControl>();
            UIManager.Instance.OpenUI<VictoryUI>();
        }
    }

    internal void Lose()
    {
        GameManager.Instance.ChangeState(GameState.Pause);
        BotManger.Instance.ChangeStateBotNull();
        UIManager.Instance.OpenUI<LoseUI>();
        UIManager.Instance.CloseUI<JoystickControl>();
    }

    public void InitCharacter()
    {
        totalCharacters.Add(player);
        foreach (Character bot in BotManger.Instance.BotsActive)
        {
            totalCharacters.Add(bot);
        }
    }

}

