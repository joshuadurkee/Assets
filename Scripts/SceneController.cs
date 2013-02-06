//------------------------------------------------------------
//****  This script manages the scene.                    ****
//****  It also hold global variables, global functions,  ****
//****  and other things important to the entire scene.   ****
//------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {
	
	//--------------------------------------------------------
	//Global Variables
	public float cubeSize;
	public float baseLayerHeight;
	public float objectCheckingBuffer;
	//--------------------------------------------------------
	
	public GameObject baseFieldParent;
	public GameObject baseCube;
	GameObject newBaseCube;
	public int halfBaseCubesWidth;
	public int halfBaseCubesLength;

	// Use this for initialization
	void Start () {
		
		baseFieldParent = GameObject.Find("Parent-BaseCubes");
		
		for(int i=-halfBaseCubesWidth; i<halfBaseCubesWidth; i++)
		{
			for(int j=-halfBaseCubesLength; j<halfBaseCubesLength; j++)
			{
				newBaseCube = (GameObject)Instantiate(baseCube, new Vector3((i*cubeSize)+(cubeSize/2),baseLayerHeight-(cubeSize/2),(j*cubeSize)+(cubeSize/2)) , Quaternion.identity);
				newBaseCube.transform.parent = baseFieldParent.transform;
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool ObjectInCubeArea (GameObject obj, Vector3 pos, float width)
	{
		width += objectCheckingBuffer;
		return (obj.transform.position.x > pos.x - width &
	 			obj.transform.position.x < pos.x + width &
	 			obj.transform.position.y > pos.y - width &
	 			obj.transform.position.y < pos.y + width &
	 			obj.transform.position.z > pos.z - width &
	 			obj.transform.position.z < pos.z + width );
	}
}
