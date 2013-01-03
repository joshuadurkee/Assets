using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {
	
	public string GrowScript;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Destroy(this.gameObject);
		}
	}
}
