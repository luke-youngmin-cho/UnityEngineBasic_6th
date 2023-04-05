using System;

public class Player
{
    public static Player instance
    {
        get
        {
            if (_instance == null)
                _instance = new Player();
            return _instance;
        }
    }
    private static Player _instance;

    public Player()
    {
        life = GameManager.instance.levelData.life;
        money = GameManager.instance.levelData.money;
    }

    public int life
    {
        get
        {
            return _life;
        }
        set
        {
            _life = value;
            onLifeChanged?.Invoke(value);
            
            if (value <= 0)
            {
                GameManager.instance.FailLevel();
            }
        }
    }
    private int _life;
    public event Action<int> onLifeChanged;

    public int money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            onMoneyChanged?.Invoke(value);
        }
    }
    private int _money;
    public event Action<int> onMoneyChanged;
}