using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    int score = 0;
    int health = 400;

    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleTon();
    }

    private void SetUpSingleTon()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public int GetHealth()
    {
        if (FindObjectOfType<Player>() != null)
            health = FindObjectOfType<Player>().PlayerHealth();
        else
            health = 0;

        return health;
    }

    public void DamageHealth(int dmgValue)
    {
        if (health - dmgValue <= 0)
            health = 0;
        else
            health -= dmgValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
