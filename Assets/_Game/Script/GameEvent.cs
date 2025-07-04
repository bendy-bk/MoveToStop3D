using UnityEngine;
using UnityEngine.Events;

public class GameEvent
{
    private readonly static UnityEvent _onChangeUserData = new UnityEvent();
    private readonly static UnityEvent _onResetListWeapon = new UnityEvent();
    private readonly static UnityEvent<Transform> _onCoinFly = new UnityEvent<Transform>();

    public static UnityEvent<Transform> OnCoinFly => _onCoinFly;
    public static UnityEvent OnChangeUserData => _onChangeUserData;
    public static UnityEvent OnResetListWeapon => _onResetListWeapon;

}

