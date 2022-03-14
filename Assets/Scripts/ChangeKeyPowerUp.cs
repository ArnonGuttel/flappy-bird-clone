using UnityEngine;
using Random = UnityEngine.Random;

public class ChangeKeyPowerUp : MonoBehaviour
{
    [SerializeField] private float speed;
    private AudioSource _audio;
    
    // Start is called before the first frame update
    void Start()
    {
        var yPos =  Random.Range(-4f, 4f);
        gameObject.transform.position = new Vector3(10, yPos, 0);
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curPosition = transform.position;
        curPosition.x -= Time.deltaTime * speed;
        gameObject.transform.position = curPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bird"))
        {
            GameManager.InvokePowerUpTaken();
            GameManager.InvokeGenerateKey();
            _audio.Play();
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            Destroy(gameObject,0.3f);
        }

        if (other.CompareTag("Wall"))
            Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
