using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacupBehavior : MonoBehaviour 
{
	// public BoxCollider2D fillTriggerLine;
	private int teaCount;
	private int teaGoal = 8; 	//can randomize this
	private bool isFull;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		if (teaCount == 8) Debug.Log("Full: " + name);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.name.Equals("Tea")) teaCount ++;
	}

	public void ResetTeaCount() { teaCount = 0; }

	public int GetTeaCount() { return teaCount; }

	public bool IsCupFull() { return teaCount >= teaGoal; }
}