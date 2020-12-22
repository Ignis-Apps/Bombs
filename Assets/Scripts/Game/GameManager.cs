﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEvent
{
    RESET_GAME,
    CHANGE_TIME
}


public class GameManager : Singleton<GameManager>
{
    public bool IsGameRunning;

    private int collectedCoins;
    private int collectedScorePoints;
    private float survivedSecounds;

    private float daytime =0.1f;
    
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

    public float getDaytime()
    {
        return daytime;
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


        // Delete all old objects
        List<GameObject> deleteList = new List<GameObject>();
        deleteList.AddRange(GameObject.FindGameObjectsWithTag("Coin"));
        deleteList.AddRange(GameObject.FindGameObjectsWithTag("ScoreOrb"));
        deleteList.AddRange(GameObject.FindGameObjectsWithTag("Bomb"));
        foreach (GameObject o in deleteList) { Destroy(o); }

        // Reset player position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector2(0, player.transform.position.y);
        
    }

    public void handleGameEvent(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case GameEvent.RESET_GAME:
                resetStats();
                break;


            case GameEvent.CHANGE_TIME:
                daytime += 0.25f;
                daytime %= 1f;
                break;
            default:
                break;
        }
    }

}
