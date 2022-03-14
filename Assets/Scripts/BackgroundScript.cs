
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    [Range(-1f,1f)]
    [SerializeField] private float scrollingSpeed = 0.5f;

    private float offset;

    private Material mat;
    
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    
    void Update()
    {
        offset += (Time.deltaTime * scrollingSpeed) / 10f;
        mat.SetTextureOffset("_MainTex",new Vector2(offset,0));
    }
}
