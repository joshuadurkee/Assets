using UnityEngine;
using System.Collections;

public class PlayerCubeControl : MonoBehaviour {
	
	public GameObject cube;
	string CubeGrowScriptName;
	AbstractCubeGrow CubeGrowScript;
	int playerRange;
	
	// Use this for initialization
	void Start () {
		playerRange = Camera.mainCamera.GetComponent<SceneController>().playerRange;
	}

	
	void GetFocusedGrowScript ()
	{
		GetFocusedCubeGrowScript ();
	}
	
	
	// Update is called once per frame
	void Update () {
		
		if (Time.timeScale != 0f)
		{
			if (Input.GetMouseButtonDown(0) && Camera.mainCamera.GetComponent<SceneController>().cubeCharges > 0)
			{
				GetFocusedGrowScript();
				if(CubeGrowScript != null) { CubeGrowScript.Invoke("growCube",0); }
			}
			
			if (Input.GetMouseButtonDown(1))
			{
				GetFocusedGrowScript();
				if(CubeGrowScript != null) { CubeGrowScript.Invoke("shrinkCube",0); }
			}
		}
	}

	void GetFocusedCubeGrowScript ()
	{
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay(new Vector3(camera.pixelWidth/2, camera.pixelHeight/2, 0));
		if (Physics.Raycast(ray, out hit, playerRange))
		{
			if (hit.collider.gameObject.tag == "CubeSide")
			{
				CubeGrowScriptName = hit.collider.gameObject.transform.parent.GetComponent<CubeController>().GrowScript;
				CubeGrowScript = (AbstractCubeGrow)hit.collider.gameObject.GetComponent(CubeGrowScriptName);
			}
			else { CubeGrowScript = null; }
		}
		else { CubeGrowScript = null; }
	}
}