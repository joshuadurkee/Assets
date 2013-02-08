using UnityEngine;
using System.Collections;
//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
//This line should always be present at the top of scripts which use pathfinding
using Pathfinding;

public class AstarAI : MonoBehaviour
{
    //The point to move to
    public Vector3 targetPosition;
	private Seeker seeker;
    private CharacterController controller;
	//The calculated path to follow
    public Path path;
	//The AI's speed per second
    public float speed = 100;
	//The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;
	//The waypoint we are currently moving towards
    private int currentWaypoint = 0;
	//A reference to the player
	public GameObject player;
	//The distance between the AI object & the player object
	public float distanceToPlayer;
	//The distance within which to attack the player
	public float attackDistance;
	//Should the AI attack the player?
	public bool attackPlayer;
	//The name of the current behavior function
	public string behavior;
	
	Vector3 dir;
	
	 
	public void Start ()
	{
		//Find the player gameobject & link it to player
		player = GameObject.FindGameObjectWithTag("Player");
		//Find the seeker component & link it to seeker
		seeker = GetComponent<Seeker>();
		//Find the character controller component & link it to controller
		controller = GetComponent<CharacterController>();
		//Repeatedly invoke the function to recalculate the path every second
		InvokeRepeating("RecalcPath",0,1);
		//Set the beginning bevahior
		behavior = "Attack";
	}
	
	public void OnPathComplete (Path p)
	{
		Debug.Log ("Yay, we got a path back. Did it have an error? "+p.error);
		if (!p.error)
		{
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
		} 
	}
	
	public void FixedUpdate ()
	{
		print("Invoking: " + behavior);
		Invoke(behavior, 0);
	}
	
	public void RecalcPath()
	{
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (transform.position,targetPosition, OnPathComplete);
		return;
	}
	
	//AI is chasing and attacking the player
	void Attack()
	{
		print("Attacking...");
		
		//Set the player as the target
		targetPosition = player.transform.position;   
		
		if (path != null)
		{
			//Calculate direction to the next waypoint  
			dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
			//Set magnitude based on speed
			dir *= speed * Time.fixedDeltaTime; 
			//Move towards the next waypoint
			controller.SimpleMove (dir);        
			//Check if we are close enough to the next waypoint      
			//If we are, proceed to follow the next waypoint       
			if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) 
			{          
				currentWaypoint++;          
				return;    
			}
		}	
		else
		{      
			//We have no path to move after
			return;     
		}        
//		
//		if (currentWaypoint >= path.vectorPath.Length) 
//		{       
//			Debug.Log ("End Of Path Reached");
//			behavior = "Stand";
//			return;       
//		}
		
		//Check if the enemy can still see the player
		dir = (player.transform.position-transform.position).normalized;
		RaycastHit hit;
		if (Physics.Raycast(transform.position, dir, out hit, attackDistance))
		{
			if (hit.collider.gameObject.tag == "Player") 
				{ behavior = "Attack"; }
			else 
				{ behavior = "Move"; }
		}
	}
	
	//AI is wandering
	void Wander()
	{
		print("Wandering...");
	}
	
	//AI has a target, but can not see the player
	//AI moves to the target
	//May not need this behavior
	void Move()
	{
		print("Moving...");
	}
	
	//AI has reached target and is standing, will get a wander target soon.
	//May not need this behavior
	void Stand()
	{
		print("Standing...");
	}
}