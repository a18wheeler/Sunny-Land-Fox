using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Events;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class Enemy : MonoBehaviour {

<<<<<<< HEAD

	public Transform target;
=======
	public CharacterController2D controller;

	public Transform target;
	private bool m_FacingRight = true;
>>>>>>> paul-Player-Movement
	// How many times each second we will update our path
	public float updateRate = 2f;
	
	private Seeker seeker;
	private Rigidbody2D rb;
	
	//The calculated path
	public Path path;
	
	//The AI's Speed per second
	public float speed = 300f;
	public ForceMode2D fMode;
	
	[HideInInspector]
	public bool pathIsEnded = false;
	

	
	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;
	
	//The Waypoint we are currently moving towards
	private int currentWayPoint = 0;
	
	void Start(){
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
		
		
		//Start a new path to the target position, return the result to the OnPathComplete method
		//seeker.StartPath(transform.position, transform.position, OnPathComplete);
			
		StartCoroutine(UpdatePath());
			
	}
	
	IEnumerator UpdatePath(){
		
		
		//Start a new path to the target position, return the result to the OnPathComplete method
		seeker.StartPath(transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds(1f / updateRate);
		StartCoroutine(UpdatePath());
		
	}
		
	public void OnPathComplete(Path p){
		Debug.Log("We got a path. Did it have an error? " + p.error);
		if(!p.error){
			path = p;
			currentWayPoint = 0;
		}
	}

	void FixedUpdate()
	{

		if(target == null)
			return;
		
		if(path == null)
			return;
		
		if(currentWayPoint >= path.vectorPath.Count){
			if(pathIsEnded)
				return;
			
			Debug.Log("End of Path Reached");
			pathIsEnded = true;
			return;
		}
		
		pathIsEnded = false;
		
		//Direction to the next way point
		Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;

		//Move the AI
		rb.AddForce(dir, fMode);
		
		if(Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]) < nextWaypointDistance)
		{
			currentWayPoint++;
			return;
		}
		

	}


}
