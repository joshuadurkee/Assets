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
    public float nextWaypointDistance = 1;
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
	//Can the AI see the player?
	public bool seePlayer;
	//The name of the current behavior function
	public string behavior;
	
	Vector3 dir;
	
	//variables for the wander behavior
	int wanderTimer;
	int wanderTime;
	int wanderTimeVariation;
	int wanderDistance;
	 
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
		behavior = "Move";
		//Preset path to avoid errors
		path = null;
		
		wanderTimer = 0;
		wanderTime = 600;
		wanderTimeVariation = Random.Range(0, 500);
		wanderDistance = 20;
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
		//Check if the enemy can still see the player
		dir = (player.transform.position-transform.position).normalized;
		RaycastHit hit;
		if (Physics.Raycast(transform.position, dir, out hit, attackDistance))
		{
			if (hit.collider.gameObject.tag == "Player") 
				{ seePlayer = true; }
			else 
				{ seePlayer = false; }
		}
		else 
			{ seePlayer = false; }
		
		if (seePlayer)
			{ behavior = "Attack"; }
		if (path == null)
			{ behavior = "Stand"; }
		else
		{
			if (path.error == true)
				{ behavior = "Stand"; }
		}
		
//		print("Invoking: " + behavior);
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
			if (currentWaypoint >= path.vectorPath.Length) 
			{       
				Debug.Log ("End Of Path Reached");
				behavior = "Stand";
			}
			else { currentWaypoint++; } 
		}
		
		//Check for conditions to cahnge states
		if (!seePlayer) { behavior = "Move"; }
		
		return;
	}
	
	//AI has a target, but can not see the player
	//AI moves to the target
	//May not need this behavior
	void Move()
	{
		print("Moving...");

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
			if (currentWaypoint >= path.vectorPath.Length-1) 
			{       
				Debug.Log ("End Of Path Reached");
				behavior = "Stand";
			}
			else { currentWaypoint++; }
		}
		
		return;
	}
	
	//AI has reached target and is standing, will get a wander target soon.
	//May not need this behavior
	void Stand()
	{
		print("Standing...");
		if (wanderTimer < wanderTime - wanderTimeVariation)
		{
			wanderTimer ++;
		}
		else
		{
			behavior = "Wander";
			wanderTimer = 0;
			wanderTimeVariation = Random.Range(0, 500);
		}
	}
	
	//AI is wandering
	void Wander()
	{
		print("Wandering...");
		Vector3 variation = new Vector3(Random.Range(0,wanderDistance),0 ,Random.Range(0,wanderDistance));
		targetPosition = this.transform.position + variation;
		behavior = "Move";
	}
}