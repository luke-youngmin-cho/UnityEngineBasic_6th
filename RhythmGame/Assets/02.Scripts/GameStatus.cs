using System.Collections;
using UnityEngine;

public static class GameStatus
{
    public static int currentCombo
    {
        get => _currentCombo;
        set
        {
            maxCombo = maxCombo > value ? maxCombo : value;
            _currentCombo = value;
        }
    }
    public static int maxCombo { get; set; }
    public static int score
    {
        get => _score;
        set
        {
            _score = value;
            ScoringText.instance.score = value;
        }
    }

    public static int coolCount;
    public static int greatCount;
    public static int goodCount;
    public static int missCount;
    public static int badCount;

    private static int _currentCombo;
    private static int _score;
    
    public static void Clear()
    {
        currentCombo = 0;
        score = 0;
        coolCount = 0;
        greatCount = 0;
        goodCount = 0;
        missCount = 0;
        badCount = 0;
    }

    public static void IncreaseCoolCount()
    {
        coolCount++;
        currentCombo++;
        score += PlaySettings.SCORE_COOL;
    }
    public static void IncreaseGreatCount()
    {
        greatCount++;
        currentCombo++;
        score += PlaySettings.SCORE_GREAT;
    }
    public static void IncreaseGoodCount()
    {
        goodCount++;
        currentCombo++;
        score += PlaySettings.SCORE_GOOD;
    }
    public static void IncreaseMissCount()
    {
        missCount++;
        currentCombo = 0;
        score += PlaySettings.SCORE_MISS;
    }
    public static void IncreaseBadCount()
    {
        badCount++;
        currentCombo = 0;
        score += PlaySettings.SCORE_BAD;
    }
}