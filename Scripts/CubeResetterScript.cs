using UnityEngine;
using System.Collections;

public class CubeResetterScript : MonoBehaviour {
	
	GameObject newCubeParent;
	
	// Use this for initialization
	void Start () {
		newCubeParent = GameObject.Find("Parent-NewCubes");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			for(int i=0; i < newCubeParent.transform.childCount; i++)
			{
				Destroy(newCubeParent.transform.GetChild(i).gameObject);
			}
			Camera.mainCamera.GetComponent<SceneController>().cubeCharges = Camera.mainCamera.GetComponent<SceneController>().maxCubeCharges;
			Camera.mainCamera.GetComponent<SceneController>().respawnLocation = transform.position;
		}
	}
}
