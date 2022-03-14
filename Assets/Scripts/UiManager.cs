using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField] private Sprite w;
    [SerializeField] private Sprite a;
    [SerializeField] private Sprite s;
    [SerializeField] private Sprite d;
    [SerializeField] private Sprite space;
    
    [SerializeField] private Sprite up;
    [SerializeField] private Sprite down;

    [SerializeField] private GameObject ChangeKeyMSG;
    [SerializeField] private Image ChangeKeyImage;
    
    [SerializeField] private GameObject ChangeGravityMSG;
    [SerializeField] private Image ChangeGravityImage;
    
    [SerializeField] private GameObject StartMsg;

    [SerializeField] private TextMeshProUGUI GravityChangeTimer;
    [SerializeField] private TextMeshProUGUI KeyChangeTimer;

    #region Fields

    private float _changeDelay = 0;
    private bool _gravityChangeFlag;
    
    #endregion
    
    
    // Start is called before the first frame update
    void Start()
    {
        ChangeGravityImage.sprite = down;
    }

    private void Awake()
    {
        GameManager.StartGame += hideStartMsg;
        GameManager.UpdateUi += displayChagneKeyMSG;
        GameManager.changeGravity += displayChagneGravtyMSG;
    }

    private void OnDestroy()
    {
        GameManager.StartGame -= hideStartMsg;
        GameManager.UpdateUi -= displayChagneKeyMSG;
        GameManager.changeGravity -= displayChagneGravtyMSG;
    }

    private void displayChagneKeyMSG()
    {
        if (GameManager.nextKey == "w")
            ChangeKeyImage.sprite = w;
        
        else if (GameManager.nextKey == "a")
            ChangeKeyImage.sprite = a;

        else if (GameManager.nextKey == "s")
            ChangeKeyImage.sprite = s;
        
        else if (GameManager.nextKey == "d")
            ChangeKeyImage.sprite = d;
        
        else if (GameManager.nextKey == "space")
            ChangeKeyImage.sprite = space; 
        
        ChangeKeyMSG.SetActive(true);
        
        _changeDelay = 3;
        _gravityChangeFlag = false;
        
        Invoke("changeCurrentKey",3);

    }

    private void displayChagneGravtyMSG()
    {
        if (ChangeGravityImage.sprite == down)
            ChangeGravityImage.sprite = up;
        else
            ChangeGravityImage.sprite = down;
        
        ChangeGravityMSG.SetActive(true);
        
        _changeDelay = 3;
        _gravityChangeFlag = true;
        
        Invoke("changeCurrentGravity",3);
    }

    private void changeCurrentKey()
    {
        ChangeKeyMSG.SetActive(false);
    }
    
    private void changeCurrentGravity()
    {
        ChangeGravityMSG.SetActive(false);
    }

    private void hideStartMsg()
    {
        StartMsg.SetActive(false);
    }

    private void Update()
    {
        if (_changeDelay > 0)
        {
            TextMeshProUGUI temp;
            string message;
            if (_gravityChangeFlag)
            {
                temp = GravityChangeTimer;
                message = "Gravity Change In: ";
            }
            else
            {
                temp = KeyChangeTimer;
                message = "Key Change In: ";
            }

            temp.gameObject.SetActive(true);
            temp.text = message + Mathf.Round(_changeDelay);
            _changeDelay -= Time.deltaTime;
            if (_changeDelay<=0)
                temp.gameObject.SetActive(false);
        }
    }
} 
