using UnityEngine;
using System.Collections;

public class BasicCubeGrow : AbstractCubeGrow {
	
	public Vector3 offset;
	public GameObject newCube;
	public GameObject player;
	public GameObject newCubeParent;
	public GameObject enemyParent;
	GameObject createdCube;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		newCubeParent = GameObject.Find("Parent-NewCubes");
		enemyParent = GameObject.Find("Parent-Enemies");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void growCube()
	{
		//Is the player in the way of a new cube?
		if(!Camera.mainCamera.GetComponent<SceneController>().ObjectInCubeArea(player,this.gameObject.transform.parent.position + offset, Camera.mainCamera.GetComponent<SceneController>().cubeSize/2))
		{
			//Player is not in the way, is there an enemy in the way?
			//Cycle through all enemies
			for (int i=0; i<enemyParent.transform.childCount; i++)
			{
				//Is this enemy in the way?
				if (Camera.mainCamera.GetComponent<SceneController>().ObjectInCubeArea(enemyParent.transform.GetChild(i).gameObject,this.gameObject.transform.parent.position + offset, Camera.mainCamera.GetComponent<SceneController>().cubeSize/2))
				{
					//This enemy is in the way.
					bool enemyDestroyed = false;
					//Is there a block on the other side of the enemy?
					//Cycle through all blocks
					for (int j=0; j<newCubeParent.transform.childCount; j++)
					{
						//is this block in the right position?
						if(this.transform.parent.transform.position + 2*offset == newCubeParent.transform.GetChild(j).transform.position)
						{
							//Enemy is in the way and another block is on the other side of the enemy
							//Destroy the enemy
							Destroy(enemyParent.transform.GetChild(i).gameObject);
							enemyDestroyed = true;
						}
					}
					//Was the enemy destroyed?
					if (!enemyDestroyed)
					{
						//Enemy was in the way, but not destroyed, move him over instead
						enemyParent.transform.GetChild(i).transform.position += offset;
					}
				}
			}
			
			createdCube = (GameObject)MonoBehaviour.Instantiate(newCube, this.gameObject.transform.parent.position + offset, Quaternion.identity);
			createdCube.transform.parent = newCubeParent.transform;
		}
		else { print("Player in the way..."); }
	}
	
	void shrinkCube()
	{
			Destroy(this.gameObject.transform.parent.gameObject);
	}
}
