using UnityEngine;

public class HalfWallScript : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(transform.parent.gameObject);
    }
}
