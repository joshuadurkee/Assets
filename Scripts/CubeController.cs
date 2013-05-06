using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {
	
	public string GrowScript;
	public GameObject player;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Camera.mainCamera.GetComponent<SceneController>().cubeCharges++;
			Destroy(this.gameObject);
		}
	}
	*/
}
