using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class BankSystem : GenericSingleton<BankSystem>
{
    public int Gem { get { return UserDataManager.Instance.UserData.Gem; } }
    public int Coin { get { return UserDataManager.Instance.UserData.Coin; } }

    public void DepositGem(int value)
    {
        UserDataManager.Instance.UserData.Gem += value;
        SaveData();
    }

    public void DepositCoin(int value)
    {
        UserDataManager.Instance.UserData.Coin += value;
        SaveData();
    }
    public bool WithdrawGem(int value)
    {
        if (UserDataManager.Instance.UserData.Gem < value)
        {
            return false;
        }
        UserDataManager.Instance.UserData.Gem -= value;
        SaveData();
        return true;
    }


    public bool WithdrawCoin(int value)
    {
        if (UserDataManager.Instance.UserData.Coin < value)
        {
            return false;
        }
        UserDataManager.Instance.UserData.Coin -= value;
        SaveData();
        return true;
    }

    public void SaveData()
    {
        GameEvent.OnChangeUserData.Invoke();
        UserDataManager.Instance.SaveData();
    }

}
