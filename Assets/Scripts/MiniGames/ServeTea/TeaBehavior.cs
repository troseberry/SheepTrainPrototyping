using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaBehavior : MonoBehaviour 
{
	private Vector3 startPosition;

	void Start () 
	{
		startPosition = transform.position;
	}
	
	void Update () 
	{
		
	}

	void SendBackToTeapot()
	{
		transform.position = TeapotBehavior.TeapotReference.GetTeaSpawnPosition();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name.Equals("TeaCatcher"))
		{
			SendBackToTeapot();
		}
	}

	public void ResetStartPosition()
	{
		transform.position = startPosition;
	}
}