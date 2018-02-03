using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacupBehavior : MonoBehaviour 
{
	// public BoxCollider2D fillTriggerLine;
	private int teaCount;
	private int teaGoal = 8; 	//can randomize this
	private bool isFull;
	
	private SpriteRenderer leftSide, rightSide, bottomSide;

	public Color startColor, finishedColor;

	void Start () 
	{
		leftSide = transform.parent.Find("Left").GetComponent<SpriteRenderer>();
		rightSide = transform.parent.Find("Right").GetComponent<SpriteRenderer>();
		bottomSide = transform.parent.Find("Bottom").GetComponent<SpriteRenderer>();
	}
	
	void Update () 
	{
		if (teaCount >= teaGoal)
		{
			// Debug.Log("Full: " + name);

			leftSide.color = finishedColor;
			rightSide.color = finishedColor;
			bottomSide.color = finishedColor;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.name.Equals("Tea")) teaCount ++;
	}

	public void ResetTeacup()
	{
		teaCount = 0;
		
		leftSide.color = startColor;
		rightSide.color = startColor;
		bottomSide.color = startColor;
	}

	public int GetTeaCount() { return teaCount; }

	public bool IsCupFull() { return teaCount >= teaGoal; }
}