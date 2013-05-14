using UnityEngine;
using System.Collections;

public class GraphUpdaterScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		InvokeRepeating("UpdateGraph",0,1);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
	
	public void UpdateGraph()
	{
		GetComponent<GraphUpdateScene>().Apply();
		Debug.Log("Graph updated automatically");
	}
}
