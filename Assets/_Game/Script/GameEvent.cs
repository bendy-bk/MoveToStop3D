using UnityEngine.Events;

public class GameEvent
{
    private readonly static UnityEvent _onChangeUserData = new UnityEvent();
    private readonly static UnityEvent _onResetListWeapon = new UnityEvent();

    public static UnityEvent OnChangeUserData => _onChangeUserData;
    public static UnityEvent OnResetListWeapon => _onResetListWeapon;
}

