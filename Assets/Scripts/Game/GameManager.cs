using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEvent
{
    RESET_GAME
}


public class GameManager : Singleton<GameManager>
{
    public bool IsGameRunning;

    private int collectedCoins;
    private int collectedScorePoints;
    private float survivedSecounds;
    
    private int dodgedBombs;
    private int survivedWaves;

    public int getCollectedCoins()
    {
        return collectedCoins;
    }

    public int getCollectedScorePoints()
    {
        return collectedScorePoints;
    }

    public float getSurvivedSecounds()
    {
        return survivedSecounds;
    }

    public void increaseCoins(int value)
    {
        collectedCoins += value;
    }

    public void increaseScorePoints(int value)
    {
        collectedScorePoints += value;
    }

    public void addDodgedBomb()
    {
        dodgedBombs += 1;
    }

    void FixedUpdate()
    {
        if (!IsGameRunning) { 
            return;       
        }

        survivedSecounds += Time.deltaTime;
    }

    public void resetStats()
    {
        collectedCoins = 0;
        collectedScorePoints = 0;
        survivedSecounds = 0;
        dodgedBombs = 0;
        survivedWaves = 0;
    }

    public void handleGameEvent(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case GameEvent.RESET_GAME:
                resetStats();
                break;

            default:
                break;
        }
    }

}
