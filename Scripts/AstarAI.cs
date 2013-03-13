using UnityEngine;
using System.Collections;
//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
//This line should always be present at the top of scripts which use pathfinding
using Pathfinding;

public class AstarAI : MonoBehaviour
{
	
	//AI gerneral attributes
	public float speed;								//The AI's movement speed
	public float rotateSpeed;						//The AI's rotation speed
	public string behavior;							//The name of the current behavior function
	
	//Pathfinding variables
	private Seeker seeker;							//The attached seeker component
	private int currentWaypoint;					//The waypoint we are currently moving towards
    public Vector3 targetPosition;					//The point to move to
    public Path path;								//The calculated path to follow
    public float nextWaypointDistance;				//The max distance from the AI to a waypoint for it to continue to the next waypoint
	
	//Player info
	public GameObject player;						//A reference to the player
	public float distanceToPlayer;					//The distance between the AI object & the player object
	public bool seePlayer;							//Can the AI see the player?
	
	//Variables for chasing
	public float chaseDistance;						//The distance within which to chase the player
	public bool chasePlayer;						//Should the AI chase the player?
	
	//Variables for attacking
	public float attackRange;						//The range of the enemy's attack
	public bool attackPlayer;						//Should the AI chase the player?
	public int attackStrength;						//How far the enemy launches the player
	public Vector3 attackDir;						//The direction the enemy launches the player
	public float knockbackDuration;					//The length of the knockback effect
	
	//Variables for the wander behavior
	double wanderTime;								//The next time to wander at
	public int wanderDistance;						//maximum distance to wander
	bool firstStand;
	
	//Miscellaneous variables
	private CharacterController controller;			//The attached character controller component
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
		behavior = "Stand";
		//Preset path to avoid errors
		path = null;
	}
	
	public void Update ()
	{
		distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
		if(distanceToPlayer < attackRange)
		{
			Debug.Log("Launching player");
			player.GetComponent<ImpactReciever>().dir = attackDir;
			player.GetComponent<ImpactReciever>().force = attackStrength;
			player.GetComponent<ImpactReciever>().Invoke("AddImpact",0);
		}
	}
	
	public void FixedUpdate ()
	{		
		//Check if the enemy can still see the player
		dir = (player.transform.position-transform.position).normalized;
		RaycastHit hit;
		if (Physics.Raycast(transform.position, dir, out hit, chaseDistance))
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
		else 
		{			
			if (path.error == true)
			{
				behavior = "Stand";
				animation.Play("idle");
			}
		}
		
//		Debug.Log("Invoking: " + behavior);
		Invoke(behavior, 0);
	}
	
//	void OnControllerColliderHit(ControllerColliderHit hit)
//	{
//		Debug.Log("collision");
//		if (hit.gameObject.tag == "Player")
//		{
//			Debug.Log("Launching player");
//			hit.collider.rigidbody.AddForce(attackDir*attackStrength);
//		}
//	}
//	void OnCollisionEnter(Collision collision)
//	{
//		Debug.Log("collision");
//		if (collision.gameObject.tag == "Player")
//		{
//			Debug.Log("Launching player");
//			collision.rigidbody.AddForce(attackDir*attackStrength);
//		}
//	}
	
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
	
	public void RecalcPath()
	{
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (transform.position,targetPosition, OnPathComplete);
		return;
	}
	
	IEnumerator Knockback()
	{
	    var startTime = Time.time;
	    while(Time.time < (startTime + knockbackDuration))
		{
	        controller.SimpleMove(attackDir*attackStrength);
	        yield return null;
	    }
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
		Debug.Log("Attacking...");
		
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
		Debug.Log("Moving...");

		MoveToTarget ();
		
		return;
	}
	
	//AI has reached target and is standing, will get a wander target soon.
	//May not need this behavior
	void Stand()
	{
		Debug.Log("Standing...");
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
		Debug.Log("Wandering...");
		Vector3 variation = new Vector3(Random.Range(-wanderDistance,wanderDistance),0 ,Random.Range(-wanderDistance,wanderDistance));
		targetPosition = this.transform.position + variation;
		behavior = "Move";
	}
}