using UnityEngine;
using System.Collections;
//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
//This line should always be present at the top of scripts which use pathfinding
using Pathfinding;

public class AstarAI : MonoBehaviour
{
	private Seeker seeker;							//The attached seeker component
    private CharacterController controller;			//The attached character controller component
	private int currentWaypoint;					//The waypoint we are currently moving towards

    public Vector3 targetPosition;					//The point to move to
    public Path path;								//The calculated path to follow
    public float speed;								//The AI's movement speed
	public float rotateSpeed;						//The AI's rotation speed
    public float nextWaypointDistance;				//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public GameObject player;						//A reference to the player
	public float distanceToPlayer;					//The distance between the AI object & the player object
	public float attackDistance;					//The distance within which to attack the player
	public bool attackPlayer;						//Should the AI attack the player?
	public bool seePlayer;							//Can the AI see the player?
	public string behavior;							//The name of the current behavior function
	
	Vector3 dir;
	
	//variables for the wander behavior
	
	double wanderTime;								//The next time to wander at
	public int wanderDistance;
	bool firstStand;
	 
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
		behavior = "Stand";
		//Preset path to avoid errors
		path = null;
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
		{
			behavior = "Attack";
			animation.Play("attack");
		}
		else
			{ animation.Play("idle"); }
		
		if (path == null)
			{ behavior = "Stand"; }
		else if (path.error == true)
			{ behavior = "Stand"; }
		
//		print("Invoking: " + behavior);
		Invoke(behavior, 0);
	}
	
	public void RecalcPath()
	{
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (transform.position,targetPosition, OnPathComplete);
		return;
	}

	/// <summary>
	/// Moves to target.
	/// </summary>

	void MoveToTarget ()
	{
		//******Rotate towards the target******
		// Find the relative place in the world where the target is located
		var relativeLocation = transform.InverseTransformPoint(path.vectorPath[currentWaypoint]);
		var angle = Mathf.Atan2 (relativeLocation.x, relativeLocation.z) * Mathf.Rad2Deg;
		// Clamp it with the max rotation speed so he doesn't move too fast
		var maxRotation = rotateSpeed * Time.deltaTime;
		var clampedAngle = Mathf.Clamp(angle, -maxRotation, maxRotation);	
		// Rotate
		transform.Rotate(0, clampedAngle, 0);
		
		//******Move forward******
		controller.SimpleMove (transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime); 
		
		
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
	}
	
	//AI is chasing and attacking the player
	void Attack()
	{
		print("Attacking...");
		
		//Set the player as the target
		targetPosition = player.transform.position;   
		
		MoveToTarget ();
		
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

		MoveToTarget ();
		
		return;
	}
	
	//AI has reached target and is standing, will get a wander target soon.
	//May not need this behavior
	void Stand()
	{
		print("Standing...");
		if (wanderTime < Time.time)
		{
			behavior = "Wander";
			firstStand = true;
		}
		else if (firstStand)
		{
			wanderTime = Time.time + Random.value * 10.0;
			firstStand = false;
		}
	}
	
	//AI is wandering
	void Wander()
	{
		print("Wandering...");
		Vector3 variation = new Vector3(Random.Range(-wanderDistance,wanderDistance),0 ,Random.Range(-wanderDistance,wanderDistance));
		targetPosition = this.transform.position + variation;
		behavior = "Move";
	}
}