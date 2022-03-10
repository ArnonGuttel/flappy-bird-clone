using UnityEngine;

public class GravityPowerUp : MonoBehaviour
{
    [SerializeField] private float speed;
    
    void Start()
    {
        var yPos =  Random.Range(-4f, 4f);
        gameObject.transform.position = new Vector3(10, yPos, 0);
    }
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
            GameManager.InvokeGravityChange();
            Destroy(gameObject);
        }
        
        if (other.CompareTag("Wall"))
            Destroy(gameObject);
        
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
