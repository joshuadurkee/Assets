using UnityEngine;
using System.Collections;

public class IndestructableCubeGrow : AbstractCubeGrow {

	public Vector3 offset;
	public Animation grow;
	public Animation shrink;
	public GameObject newCube;
	public GameObject player;
	public GameObject newCubeParent;
	GameObject createdCube;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		newCubeParent = GameObject.Find("Parent-NewCubes");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void growCube()
	{
		if(!(player.transform.position.x > (this.gameObject.transform.parent.position + offset).x - 2 &
			 player.transform.position.x < (this.gameObject.transform.parent.position + offset).x + 2 &
			 player.transform.position.y > (this.gameObject.transform.parent.position + offset).y - 2 &
			 player.transform.position.y < (this.gameObject.transform.parent.position + offset).y + 2 &
			 player.transform.position.z > (this.gameObject.transform.parent.position + offset).z - 2 &
			 player.transform.position.z < (this.gameObject.transform.parent.position + offset).z + 2 ))
		{
			createdCube = (GameObject)MonoBehaviour.Instantiate(newCube, this.gameObject.transform.parent.position + offset, Quaternion.identity);
			createdCube.transform.parent = newCubeParent.transform;
		}
		else { print("Fail"); }
	}
	
	void shrinkCube()
	{
			//Destroy(this.gameObject.transform.parent.gameObject);
	}
}
