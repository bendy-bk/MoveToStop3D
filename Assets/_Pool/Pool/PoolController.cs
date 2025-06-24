﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    [SerializeField] private PoolAmount[] poolAmounts;

    private void Awake()
    {
        for (int i = 0; i < poolAmounts.Length; i++)
        {
            SimplePool.PreLoad(poolAmounts[i].prefab, poolAmounts[i].amount, poolAmounts[i].parent);
        }
    }

}

[System.Serializable]
public class PoolAmount
{
    public GameUnit prefab;

    public Transform parent;

    public int amount;

}


