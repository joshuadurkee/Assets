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
	public int   cubeCharges;
	public int   maxCubeCharges;
	//--------------------------------------------------------
	
	GameObject baseFieldParent;
	GameObject newBaseCube;
	public GameObject baseCube;
	public int halfBaseFieldWidth;
	public int halfBaseFieldLength;
	public GUIStyle style;

	// Use this for initialization
	void Start () {
		
		baseFieldParent = GameObject.Find("Parent-BaseCubes");
		
		//CreatObjectField (baseFieldParent, halfBaseFieldWidth, halfBaseFieldLength);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI ()
	{
		GUI.Box(new Rect(Screen.width/20, Screen.height/20, 100, 50), cubeCharges.ToString(), style);
	}

	void CreatObjectField (GameObject obj, int halfWidth, int halfLength)
	{
		for(int i=-halfWidth; i<halfWidth; i++)
		{
			for(int j=-halfLength; j<halfLength; j++)
			{
				newBaseCube = (GameObject)Instantiate(baseCube, new Vector3((i*cubeSize)+(cubeSize/2),baseLayerHeight-(cubeSize/2),(j*cubeSize)+(cubeSize/2)) , Quaternion.identity);
				newBaseCube.transform.parent = baseFieldParent.transform;
			}
		}
	}
	
	public bool TargetInCubeArea (Vector3 target, Vector3 pos, float width)
	{
		width += objectCheckingBuffer*2;
		return (target.x > pos.x - width/2 &
	 			target.x < pos.x + width/2 &
	 			target.y > pos.y - width/2 &
	 			target.y < pos.y + width/2 &
	 			target.z > pos.z - width/2 &
	 			target.z < pos.z + width/2 );
	}
	
	public bool TargetInArea (Vector3 target, Vector3 pos, Vector3 area)
	{
		area.x += objectCheckingBuffer*2;
		area.y += objectCheckingBuffer*2;
		area.z += objectCheckingBuffer*2;
		return (target.x > pos.x - area.x/2 &
	 			target.x < pos.x + area.x/2 &
	 			target.y > pos.y - area.y/2 &
	 			target.y < pos.y + area.y/2 &
	 			target.z > pos.z - area.z/2 &
	 			target.z < pos.z + area.z/2 );
	}
}
