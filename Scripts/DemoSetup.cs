using UnityEngine;
using System.Collections;

public class DemoSetup : MonoBehaviour {
	
	public GameObject baseCube;

	// Use this for initialization
	void Start () {
		
		for(int i=-5; i<5; i++)
		{
			for(int j=-5; j<5; j++)
			{
				Instantiate(baseCube, new Vector3(i*3,0,j*3) , Quaternion.identity);
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
