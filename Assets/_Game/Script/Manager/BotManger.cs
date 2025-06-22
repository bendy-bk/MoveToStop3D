using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BotManger : GenericSingleton<BotManger>
{
    private Level curLevel;

    public List<Bot> bots = new();
    private void OnEnable()
    {
        Character.OnBotDeath += HandleBotDeath;
    }

    private void OnDisable()
    {
        Character.OnBotDeath -= HandleBotDeath;
    }
    public Level CurLevel { get => curLevel; set => curLevel = value; }

    public void SpawnBot(int amount)
    {
        //Bot
        for (int i = 0; i < amount - 1; i++)
        {
            GameObject bot = ObjectPoolManager.Instance.SpawnFromPool(PoolType.Bot, CurLevel.SpawnPoint[i]);
        }
    }

    private void HandleBotDeath(Character bot)
    {
        StartCoroutine(RespawnBotAfterDelay(curLevel.respawnPoint, 3f));
    }

    private IEnumerator RespawnBotAfterDelay(Transform pos, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject newBot = ObjectPoolManager.Instance.SpawnFromPool(PoolType.Bot, pos);

    }


}
