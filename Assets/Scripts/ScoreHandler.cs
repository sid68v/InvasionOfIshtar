using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    public static ScoreHandler Instance;

    [SerializeField] Text scoreText;
    [SerializeField] Text healthText;

    int score = 0;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetScoreText();
        SetHealthText();
    }

    void SetScoreText()
    {
        scoreText.text = "score : " + score.ToString();
    }
    public void SetHealthText()
    {
        healthText.text = "health : " + PlayerController.Instance.playerHealth.ToString();
    }
    public void UpdateScore(int scorePerEnemy)
    {
        score += scorePerEnemy;
        SetScoreText();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
