using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChangeKeyPowerUp : MonoBehaviour
{
    [SerializeField] private float speed;

    

    // Start is called before the first frame update
    void Start()
    {
        var yPos =  Random.Range(-4f, 4f);
        gameObject.transform.position = new Vector3(10, yPos, 0);
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
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
