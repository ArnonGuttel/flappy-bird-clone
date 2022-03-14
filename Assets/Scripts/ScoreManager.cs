using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI HighScoreText;
    [SerializeField] private float pointsPerSecond;
    [SerializeField] private int PowerUpPoints;

    private float currentScore;
    private float doubleScoreTime;
    private bool _startScore;


    private void Awake()
    {
        GameManager.StartGame += displayScore;
        GameManager.StartGame += startScore;
        GameManager.powerUpTaken += addPowerUpPoints;
        GameManager.GameOver += checkHighScore;
        
    }

    private void OnDestroy()
    {
        GameManager.StartGame -= startScore;
        GameManager.StartGame -= displayScore;
        GameManager.powerUpTaken -= addPowerUpPoints;
        GameManager.GameOver += checkHighScore;
    }

    private void Start()
    {
        HighScoreText.text = "high Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_startScore)
            return;
        currentScore += pointsPerSecond * Time.deltaTime;
        ScoreText.text = "Score: " + Mathf.Round(currentScore);
    }

    void addPowerUpPoints()
    {
        currentScore += PowerUpPoints;
    }

    void displayScore()
    {
        ScoreText.gameObject.SetActive(true);
        HighScoreText.gameObject.SetActive(true);
    }

    private void checkHighScore()
    {
        if (currentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", (int)currentScore);
        }
    }

    private void startScore()
    {
        _startScore = true;
    }
    
}