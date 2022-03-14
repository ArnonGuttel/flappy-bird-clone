using UnityEngine;
using Random = UnityEngine.Random;


public class WallScript : MonoBehaviour
{
    #region Inspector
    
    [SerializeField] private float speed;

    #endregion
    void Start()
    {
        float yPos = Random.Range(4, -2);
        gameObject.transform.position = new Vector3(10, yPos, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curPosition = transform.position;
        curPosition.x -= Time.deltaTime * speed;
        gameObject.transform.position = curPosition;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
    
    
}
