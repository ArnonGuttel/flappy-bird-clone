using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject changeKeyPrefab;
    [SerializeField] private float firstWallTime;
    [SerializeField] private float wallsDelay;
    [SerializeField] private float keyChangeDelayDuration;

    #endregion

    #region Fields

    private readonly String[] keys =
    {
        "backspace", "escape", "space",
        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
        "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
    };

    public static string curKey = "space";

    private string _nextKey;
    private float _keyCountdown;
    private bool _countdownStarted;

    #endregion

    #region Events

    public static event Action GenerateKey;

    public static event Action powerUpTaken;
    public static event Action GameOver;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        GameOver += Temp;
        GenerateKey += generateKey;
        
    }

    private void OnDestroy()
    {
        GameOver -= Temp;
        GenerateKey -= generateKey;
    }

    private void Start()
    {
        InvokeRepeating(nameof(CreateWall), firstWallTime, wallsDelay);
        InvokeRepeating(nameof(CreateKeyChangePowerUp), firstWallTime - 1f, wallsDelay);
        _keyCountdown = keyChangeDelayDuration;
    }

    private void Update()
    {
        if (_countdownStarted)
        {
            if (_keyCountdown <= 0)
            {
                curKey = _nextKey;
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

    private void CreateWall()
    {
        Instantiate(wallPrefab);
    }

    private void CreateKeyChangePowerUp()
    {
        Instantiate(changeKeyPrefab);
    }

    private void Temp()
    {
        print("GameOver");
    }

    private void generateKey()
    {
        if (_countdownStarted)
            return;

        _nextKey = keys[Random.Range(0, keys.Length - 1)];
        _keyCountdown = keyChangeDelayDuration;
        _countdownStarted = true;
        print(_nextKey);
    }

    #endregion
}