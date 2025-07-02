using UnityEngine.Events;

public class GameEvent
{
    private readonly static UnityEvent _onChangeUserData = new UnityEvent();

    public static UnityEvent OnChangeUserData => _onChangeUserData;
}

