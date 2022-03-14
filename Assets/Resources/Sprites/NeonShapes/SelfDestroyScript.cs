using UnityEngine;

public class SelfDestroyScript : MonoBehaviour 
{
	
	void OnEnable ()
	{
		Invoke("deactive",0.2f);
	}


	private void deactive()
	{
		gameObject.SetActive(false);
	}
}
