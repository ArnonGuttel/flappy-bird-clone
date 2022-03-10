using UnityEngine;
using Random = UnityEngine.Random;


public class WallScript : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject bottomWall;
    [SerializeField] private int gapSize;
    [SerializeField] private float speed;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        float topSize = Random.Range(0f, 8f);
        float bottomSize = 10 - gapSize - topSize;
        topWall.transform.localScale = new Vector3(topWall.transform.localScale.x,topSize);
        bottomWall.transform.localScale = new Vector3(bottomWall.transform.localScale.x,bottomSize);
        //TODO: Fix constant 
        gameObject.transform.position = new Vector3(10, 0, 0);
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
