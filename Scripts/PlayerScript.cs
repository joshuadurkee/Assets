using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	GameObject 	crosshairNoFocus;
	GameObject 	crosshairCubeFocus;
	int 		playerRange;
	int			currentFocus;
	
	// Use this for initialization
	void Start () {
		crosshairNoFocus 	= GameObject.Find("CrosshairNoFocus");
		crosshairCubeFocus 	= GameObject.Find("CrosshairCubeFocus");
		playerRange 		= Camera.mainCamera.GetComponent<SceneController>().playerRange;
		currentFocus 		= 0;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay(new Vector3(camera.pixelWidth/2, camera.pixelHeight/2, 0));
		if (Physics.Raycast(ray, out hit, playerRange))
		{
			if (hit.collider.gameObject.tag == "CubeSide")
			{
				if (currentFocus != 1) { Invoke("CubeFocused",0); }
			}
			else
			{
				if (currentFocus != 0) { Invoke("NoFocus",0); }
			}
		}
		else
		{
			if (currentFocus != 0) { Invoke("NoFocus",0); }
		}
	}
	
	void NoFocus()
	{
		//Debug.Log("No Focus");
		currentFocus = 0;
		crosshairNoFocus.guiTexture.enabled = true;
		crosshairCubeFocus.guiTexture.enabled = false;		
	}
	
	void CubeFocused()
	{
		//Debug.Log("Cube Focused");
		currentFocus = 1;
		crosshairNoFocus.guiTexture.enabled = false;
		crosshairCubeFocus.guiTexture.enabled = true;		
	}
}
