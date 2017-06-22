public enum State
{
    Menu,
    Play,
    Shuffle,
    Solved
}

public static class GameState
{
    static State currentState;

    public static State State
    {
        set
        {
            currentState = value;
        }
        get
        {
            return currentState;
        }
    }
}
