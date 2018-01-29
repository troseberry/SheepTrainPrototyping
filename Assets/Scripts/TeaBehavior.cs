using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaBehavior : MonoBehaviour 
{

	void Start () 
	{
		
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
}