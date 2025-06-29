using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BotManger : GenericSingleton<BotManger>
{
    private Level curLevel;

    [SerializeField]private List<Bot> bots = new();

    private void OnEnable()
    {
        Character.OnBotDeath += HandleBotDeath;
    } 

    public Level CurLevel { get => curLevel; set => curLevel = value; }
    public List<Bot> Bots { get => bots; set => bots = value; }

    public void SpawnBot(int amount)
    {
        //Bot
        for (int i = 0; i < amount-1; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, CurLevel.SpawnPoint[i].position, Quaternion.identity);
            Bots.Add(bot);
        }
    }

    public void HandleBotDeath(Character bot)
    {
        StartCoroutine(RespawnBotAfterDelay(5f));
    }

    private IEnumerator RespawnBotAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Transform spawnPoint = GetFreeSpawnPoint();

        if (spawnPoint != null)
        {
            Bot newBot = SimplePool.Spawn<Bot>(PoolType.Bot, spawnPoint.position, Quaternion.identity);
            Bots.Add(newBot);
        }
        else
        {
            Debug.LogWarning("Không còn vị trí spawn trống!");
        }
    }


    private Transform GetFreeSpawnPoint()
    {
        foreach (var point in curLevel.SpawnPoint)
        {
            bool occupied = false;
            foreach (var bot in Bots)
            {
                if (Vector3.Distance(bot.transform.position, point.position) < 3f) // khoảng cách nhỏ nghĩa là đang chiếm
                {
                    occupied = true;
                    break;
                }
            }

            if (!occupied)
                return point;
        }

        return null; // không có chỗ nào trống
    }

}
