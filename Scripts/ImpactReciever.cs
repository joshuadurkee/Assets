using UnityEngine;
using System.Collections;

public class ImpactReciever : MonoBehaviour {
	
	float mass = 3.0f; // defines the character's mass
	Vector3 impact = Vector3.zero;
	private CharacterController character;
	
	public Vector3 dir;
	public float force;
	
	// Use this for initialization
	void Start () {
		character = this.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		// apply the impact force:
		if (impact.magnitude > 0.2) character.Move(impact * Time.deltaTime);
		// consumes the impact energy each cycle:
		impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);
	}
	
	// call this function to add an impact force:
	void AddImpact()
	{
		dir.Normalize();
		if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
		impact += dir.normalized * force / mass;
	}
}