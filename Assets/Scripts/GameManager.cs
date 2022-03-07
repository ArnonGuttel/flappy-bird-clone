using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private float wallDelay;
    
    #endregion

    // #region Fields
    //
    // private static GameManager _shared;
    //
    // #endregion
    
    #region Events

    public static event Action GameOver;

    #endregion

    private void Awake()
    {
        GameOver += Temp;
    }

    private void OnDestroy()
    {
        GameOver -= Temp;
    }

    private void Start()
    {
        // _shared = this;
        InvokeRepeating("CreateWall",1,wallDelay);
    }

    private void CreateWall()
    {
        Instantiate(wallPrefab);
    }
    
    private void Temp()
    {
        print("GameOver");
    }
    
    public static void InvokeGameOver()
    {
        GameOver?.Invoke();
    }

}
