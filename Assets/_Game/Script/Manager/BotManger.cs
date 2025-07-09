using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BotManger : GenericSingleton<BotManger>
{
    private Level curLevel;
    [SerializeField] private float timeDelay = 5f;

    [SerializeField] private List<Bot> botsActive = new();

    [SerializeField] private List<Bot> botsInactive = new();

    private void OnEnable()
    {
        Bot.OnBotDeathBot += HandleBotDeath;
    }

    private void OnDisable()
    {
        Bot.OnBotDeathBot -= HandleBotDeath;
    }

    public Level CurLevel { get => curLevel; set => curLevel = value; }
    public List<Bot> BotsActive { get => botsActive; set => botsActive = value; }
    public List<Bot> BotsInactive { get => botsInactive; set => botsInactive = value; }

    public void SpawnBot(int amount)
    {
        //Bot
        for (int i = 0; i < amount - 1; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, CurLevel.SpawnPoint[i].position, Quaternion.identity);
            bot.SpawmPoint = curLevel.SpawnPoint[i];
            BotsActive.Add(bot);
        }
    }

    public void HandleBotDeath(Bot bot)
    {
        bot.Characters.Clear();
        SimplePool.Despawn(bot);
        botsActive.Remove(bot);
        botsInactive.Add(bot);

        if (GameManager.Instance.IsState(GameState.Gameplay))
        {
            StartCoroutine(RespawnBotAfterDelay(timeDelay, bot.SpawmPoint));
        } 
    }

    private IEnumerator RespawnBotAfterDelay(float delay, Transform pointSpawn)
    {
        // Kiểm tra pointSpawn có còn tồn tại không
        if (pointSpawn == null)
        {
            Debug.LogWarning("Spawn point bị xoá trong lúc delay.");
            yield break;
        }

        Bot newBot = SimplePool.Spawn<Bot>(PoolType.Bot, pointSpawn.position, Quaternion.identity);
        newBot.CircleTarget.SetActive(false);
        newBot.ChangeState(new IdleState());
        //
        botsInactive.Remove(newBot);
        BotsActive.Add(newBot);
    }

    public void ChangeStateBotNull()
    {
        foreach (var item in BotsActive)
        {
            item.ChangeState(null);
        }
    }

    public void ResetBotManager()
    {
        botsActive.Clear();
        botsInactive.Clear();
        SimplePool.Release(PoolType.Bot);
    }

}
