using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text scoreText;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        gameSession = GetComponent<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.Text = gameSession.GetScore().ToString();
    }
}
