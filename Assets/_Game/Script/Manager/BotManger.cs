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
    private void DisEnable()
    {
        Character.OnBotDeath -= HandleBotDeath;
    }

    public Level CurLevel { get => curLevel; set => curLevel = value; }

    public void SpawnBot(int amount)
    {
        //Bot
        for (int i = 0; i < amount-1; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, CurLevel.SpawnPoint[i].position, Quaternion.identity);
        }
    }

    public void HandleBotDeath(Character bot)
    {
        StartCoroutine(RespawnBotAfterDelay(curLevel.respawnPoint, 3f));
    }

    private IEnumerator RespawnBotAfterDelay(Transform pos, float delay)
    {
        yield return new WaitForSeconds(delay);

        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, pos.position, Quaternion.identity);

    }


}
