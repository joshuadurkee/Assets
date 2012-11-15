using UnityEngine;
using System.Collections;

public class CubeGrow : MonoBehaviour {
	
	public Vector3 offset;
	public Animation grow;
	public Animation shrink;
	public GameObject newCube;
	public GameObject player;
	public bool destructable;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void growNewCube()
	{
		if(!(player.transform.position.x > (this.gameObject.transform.parent.position + offset).x - 2 &
			 player.transform.position.x < (this.gameObject.transform.parent.position + offset).x + 2 &
			 player.transform.position.y > (this.gameObject.transform.parent.position + offset).y - 2 &
			 player.transform.position.y < (this.gameObject.transform.parent.position + offset).y + 2 &
			 player.transform.position.z > (this.gameObject.transform.parent.position + offset).z - 2 &
			 player.transform.position.z < (this.gameObject.transform.parent.position + offset).z + 2 ))
		{
			Instantiate(newCube, this.gameObject.transform.parent.position + offset, Quaternion.identity);
		}
		else { print("Fail"); }
	}
	
	void shrinkCube()
	{
		if(destructable)
		{
			Destroy(this.gameObject.transform.parent.gameObject);
		}
	}
}
