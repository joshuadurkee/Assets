using UnityEngine;
using System.Collections;

public class DeathPlaneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//When something touches the death plane
	void OnTriggerEnter(Collider other)
	{
		//Is the object the player?
		if (other.tag == "Player")
		{
			//Do something
		}
		Destroy(other.gameObject);
	}
}
