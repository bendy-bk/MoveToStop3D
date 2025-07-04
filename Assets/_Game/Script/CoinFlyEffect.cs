using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFlyEffect : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab; // Prefab đồng xu
    [SerializeField] private Transform startPoint; // Vị trí bắt đầu
    [SerializeField] private Transform targetPoint; // Header coin
    [SerializeField] private int coinCount = 10; // Số lượng coin

    [SerializeField] private Transform canvasTransform; // Canvas parent

    public void Play()
    {
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        for (int i = 0; i < coinCount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, canvasTransform);
            coin.transform.position = startPoint.position;

            // Fly to target
            coin.transform.DOMove(targetPoint.position, 0.6f)
                .SetEase(Ease.InBack)
                .OnComplete(() => Destroy(coin)); // Xoá coin sau khi bay xong

            yield return new WaitForSeconds(0.05f); // Delay giữa mỗi đồng
        }
    }
}
