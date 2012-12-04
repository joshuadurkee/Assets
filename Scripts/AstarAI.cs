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
		distanceToPlayer  = (transform.position - player.transform.position).magnitude;
		if (distanceToPlayer < attackDistance & attackPlayer)
		{
			targetPosition = player.transform.position;
		}
		
		if (path == null)
		{      
			//We have no path to move after yet  
			return;     
		}           
		if (currentWaypoint >= path.vectorPath.Length) 
		{       
			Debug.Log ("End Of Path Reached");    
			return;       
		}             
		//Direction to the next waypoint  
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;  
		dir *= speed * Time.fixedDeltaTime;   
		controller.SimpleMove (dir);        
		//Check if we are close enough to the next waypoint      
		//If we are, proceed to follow the next waypoint       
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) 
		{          
			currentWaypoint++;          
			return;    
		}   
	}
	public void RecalcPath()
	{
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (transform.position,targetPosition, OnPathComplete);
		return;
	}
}