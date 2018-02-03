using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeapotBehavior : MonoBehaviour 
{
	public static TeapotBehavior TeapotReference;

	private Vector3 startPosition;
	
	private Rigidbody2D teapotRigidbody;
	public BoxCollider2D teapotBlocker;
	public BoxCollider2D funnelBottom;

	public Transform teaSpawn;


	void Start () 
	{
		TeapotReference = this;
		startPosition = transform.position;

		teapotRigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
	{
		
	}

	public void HoldingTeapot()
	{
		Debug.Log("Holding Teapot");
		if (!teapotBlocker.isTrigger) teapotBlocker.isTrigger = true;
		PourTea();
		
	}

	void PourTea()
	{
		funnelBottom.isTrigger = true;
	}

	public void StopHolding()
	{
		funnelBottom.isTrigger = false;
	}

	public Vector3 GetTeaSpawnPosition() { return teaSpawn.position; }

	public void ResetTeapotPosition() { transform.position = startPosition; }
	
	public void ResetTeapotBlocker() { teapotBlocker.isTrigger = false; }
}