using UnityEngine;
using System.Collections;

public class PlayerCubeControl : MonoBehaviour {
	
	public GameObject cube;
	public CubeGrow CubeGrowScript;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = camera.ScreenPointToRay(new Vector3(camera.pixelWidth/2, camera.pixelHeight/2, 0));
			if (Physics.Raycast(ray, out hit, 10))
			{
				if (hit.collider.gameObject.tag == "CubeSide")
				{
					hit.collider.gameObject.GetComponent<CubeGrow>().Invoke("growNewCube",0);
				}
			}
		}
		
		if (Input.GetMouseButtonDown(1))
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5))
			{
				if (hit.collider.gameObject.tag == "CubeSide")
				{
					hit.collider.gameObject.GetComponent<CubeGrow>().Invoke("shrinkCube",0);
				}
			}
		}
		
	}
}