

public enum GameState { MainMenu, Gameplay, Pause }

public class GameManager : GenericSingleton<GameManager>
{
    private GameState _state;

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState gameState)
    {
        _state = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return _state == gameState;
    }

}
