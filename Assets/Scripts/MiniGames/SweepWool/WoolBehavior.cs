using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoolBehavior : MonoBehaviour 
{

	private bool canSwipe = true;
	private bool initiateSwipe = false;

	float swipeThreshold = 100.0f;

	Vector2 startingTouchPosition;
	Vector2 endingTouchPosition;
	
	private bool doAddImpulse;
	private float swipeSpeed = 30f;

	private Rigidbody2D woolRigidbody;

	private Vector2 startPosition;

	void Start () 
	{
		woolRigidbody = GetComponent<Rigidbody2D>();
		startPosition = transform.position;
	}
	
	void Update () 
	{
		if (initiateSwipe && canSwipe)
		{
			float prevTouchDeltaMag = (startingTouchPosition - endingTouchPosition).magnitude;

			// Debug.Log("Touch Delta: " + prevTouchDeltaMag);
			if (prevTouchDeltaMag >= swipeThreshold)
			{
				// Debug.Log("Registered Swipe Over Threshold");
				// Debug.Log("Touch Delta: " + prevTouchDeltaMag);

				doAddImpulse = true;
				initiateSwipe = false;
				SweepWool.IncrementSweptCount();
				Debug.Log("Swept: " + SweepWool.GetSweptCount());
				canSwipe = false;
			}
		}
	}


	void FixedUpdate()
	{
		if (doAddImpulse)
		{
			Vector2 direction = (endingTouchPosition - startingTouchPosition).normalized * swipeSpeed;

			// Debug.Log("Direction: " + direction);
			// Debug.Log("Direction normalized: " + direction.normalized);

			woolRigidbody.AddForce(new Vector2(direction.x, direction.y), ForceMode2D.Impulse);
			doAddImpulse = false;
		}
	}

	public void RegisterSwipe()
	{
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);
			startingTouchPosition = touch.position - touch.deltaPosition;
		}
	}

	public void EndSwpie()
	{
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);
			endingTouchPosition = touch.position - touch.deltaPosition;
		}
		initiateSwipe = true;
	}

	public void ResetPosition()
	{
		transform.position = startPosition;
	}
}