using UnityEngine;

public class FlappyScript : MonoBehaviour
{
    #region Inspector

    [SerializeField] private float jumpHeight;

    #endregion
    
    #region Fields
    
    private Rigidbody2D _rb;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        GameManager.GameOver += changeColor;
    }

    private void OnDestroy()
    {
        GameManager.GameOver -= changeColor;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(GameManager.curKey))
        {
            _rb.velocity = new Vector2(0,jumpHeight);
        }
    }

    private void OnBecameInvisible()
    {
        GameManager.InvokeGameOver();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Wall"))
            GameManager.InvokeGameOver();
    }

    #endregion

    #region Methods

    private void changeColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    #endregion
    
}
