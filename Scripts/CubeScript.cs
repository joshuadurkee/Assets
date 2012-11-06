using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {
	
	public GameObject player;
	
	// Use this for initialization
	void Start () {
//		if( player.transform.position.x > this.gameObject.transform.position.x - 2 &
//			player.transform.position.x < this.gameObject.transform.position.x + 2 &
//			player.transform.position.y > this.gameObject.transform.position.y - 2 &
//			player.transform.position.y < this.gameObject.transform.position.y + 2 &
//			player.transform.position.z > this.gameObject.transform.position.z - 2 &
//			player.transform.position.z < this.gameObject.transform.position.z + 2 )
//		{
//			Destroy(this.gameObject);
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Destroy(this.gameObject);
		}
	}
}
