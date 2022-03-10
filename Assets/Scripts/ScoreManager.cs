using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI DoubleScoreText;
    [SerializeField] private float pointsPerSecond;
    [SerializeField] private int pointsMultiply;
    [SerializeField] private int MultPointsDuration;

    private float currentScore;
    private float doubleScoreTime;


    private void Awake()
    {
        GameManager.powerUpTaken += addDoublePointsTime;
    }

    private void OnDestroy()
    {
        GameManager.powerUpTaken -= addDoublePointsTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (doubleScoreTime > 0)
        {
            DoubleScoreText.gameObject.SetActive(true);
            DoubleScoreText.text = "Time Left: " + Mathf.Round(doubleScoreTime);
            currentScore += pointsPerSecond * pointsMultiply * Time.deltaTime;
            doubleScoreTime -= Time.deltaTime;
        }
        else
        {
            DoubleScoreText.gameObject.SetActive(false);
            doubleScoreTime = 0;
            currentScore += pointsPerSecond * Time.deltaTime;
        }
        ScoreText.text = "Score: " + Mathf.Round(currentScore);;
    }

    void addDoublePointsTime()
    {
        doubleScoreTime += MultPointsDuration;
    }
}