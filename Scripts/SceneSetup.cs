using UnityEngine;
using System.Collections;

public class SceneSetup : MonoBehaviour {
	
	public GameObject baseFieldParent;
	public GameObject baseCube;
	GameObject newBaseCube;
	public int halfWidth;
	public int halfLength;

	// Use this for initialization
	void Start () {
		
		baseFieldParent = GameObject.Find("Parent-BaseCubes");
		
		for(int i=-halfWidth; i<halfWidth; i++)
		{
			for(int j=-halfLength; j<halfLength; j++)
			{
				newBaseCube = (GameObject)Instantiate(baseCube, new Vector3((float)((i*3)+1.5),(float)-1.5,(float)((j*3)+1.5)) , Quaternion.identity);
				newBaseCube.transform.parent = baseFieldParent.transform;
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
