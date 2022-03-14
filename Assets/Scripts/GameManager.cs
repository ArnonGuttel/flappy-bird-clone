using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject keyChangePrefab;
    [SerializeField] private GameObject gravityChangePrefab;
    [SerializeField] private float firstWallTime;
    [SerializeField] private float wallsDelay;
    [SerializeField] private float keyChangeDelayDuration;

    #endregion

    #region Fields

    private readonly String[] keys =
    {
        "w", "space", "a", "s", "d"
    };

    public static string curKey = "space";
    public static string nextKey;
    
    private float _keyCountdown;
    private bool _countdownStarted;

    private bool _gameStarted;

    #endregion

    #region Events

    public static event Action powerUpTaken;
    public static event Action changeGravity;
    public static event Action GenerateKey;
    public static event Action GameOver;
    public static event Action UpdateUi;
    public static event Action StartGame;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        StartGame += CreateItems;
        GameOver += ResetScene;
        GenerateKey += generateKey;
    }

    private void OnDestroy()
    {
        StartGame -= CreateItems;
        GameOver -= ResetScene;
        GenerateKey -= generateKey;
    }

    private void Start()
    {
        _keyCountdown = keyChangeDelayDuration;
        curKey = "space";
    }

    private void Update()
    {
        if (!_gameStarted)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _gameStarted = true;
                InvokeStartGame();
            }
        
        if (_countdownStarted)
        {
            if (_keyCountdown <= 0)
            {
                curKey = nextKey;
                _countdownStarted = false;
            }
            else
            {
                _keyCountdown -= Time.deltaTime;
            }
        }
    }

    #endregion

    #region Methods

    public static void InvokeGameOver()
    {
        GameOver?.Invoke();
    }

    public static void InvokeGenerateKey()
    {
        GenerateKey?.Invoke();
    }

    public static void InvokePowerUpTaken()
    {
        powerUpTaken?.Invoke();
    }

    public static void InvokeGravityChange()
    {
        changeGravity?.Invoke();
    }

    public static void InvokeStartGame()
    {
        StartGame?.Invoke();
    }

    private void InvokeUpdateUi()
    {
        UpdateUi?.Invoke();
    }


    private void CreateWall()
    {
        Instantiate(wallPrefab);
    }

    private void CreatePowerUp()
    {
        var temp = Random.Range(0, 2);
        Instantiate(temp == 0 ? keyChangePrefab : gravityChangePrefab);
    }

    private void ResetScene()
    {
       SceneManager.LoadScene("MainScene");
    }

    private void generateKey()
    {
        if (_countdownStarted)
            return;

        nextKey = keys[Random.Range(0, keys.Length - 1)];
        _keyCountdown = keyChangeDelayDuration;
        _countdownStarted = true;
        InvokeUpdateUi();
    }

    private void CreateItems()
    {
        InvokeRepeating(nameof(CreateWall), firstWallTime, wallsDelay);
        InvokeRepeating(nameof(CreatePowerUp), firstWallTime+1, wallsDelay*1.3f);
    }
    

    #endregion
}