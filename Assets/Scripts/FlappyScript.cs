using UnityEngine;

public class FlappyScript : MonoBehaviour
{
    #region Inspector

    [SerializeField] private float jumpHeight;
    [SerializeField] private AudioClip explotion;

    #endregion
    
    #region Fields
    
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sp;
    private AudioSource _audio;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        GameManager.StartGame += activeRb;
        GameManager.GameOver += changeColor;
        GameManager.changeGravity += chagneGravity;
    }

    private void OnDestroy()
    {
        GameManager.StartGame -= activeRb;
        GameManager.GameOver -= changeColor;
        GameManager.changeGravity -= chagneGravity;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sp = GetComponent<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();

    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(GameManager.curKey))
        {
            _rb.velocity = new Vector2(0,jumpHeight);
            _animator.SetTrigger("Jump");
            _audio.Play();
        }
    }

    private void OnBecameInvisible()
    {
        GameManager.InvokeGameOver();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            _audio.clip = explotion;
            _audio.Play();
            _animator.SetTrigger("Explode");
            Invoke("invokeGameOver",0.4f);
        }
    }

    #endregion

    #region Methods

    private void changeColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void chagneGravity()
    {
        Invoke("change",3);
    }

    private void change()
    {
        _rb.gravityScale *= -1;
        jumpHeight *= -1;
        _sp.flipY = !_sp.flipY;

    }

    private void activeRb()
    {
        _rb.simulated = true;
    }

    private void invokeGameOver()
    {
        GameManager.InvokeGameOver();
    }
    #endregion
    
}
