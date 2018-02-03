using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketBehavior : MonoBehaviour 
{
	Rigidbody2D ticketRigidbody;
	Vector2 randomPosition;
	float randomRotationZ;

	float xDir = 0;
	float yDir = 0;
	bool doAddImpulse = false;
	


	void Start () 
	{
		ticketRigidbody = gameObject.GetComponent<Rigidbody2D>();
		RandomizeStart();
	}
	
	void FixedUpdate () 
	{
		if (doAddImpulse)
		{
			xDir = (Random.Range(0, 2) == 1) ? -5 : 5;
			yDir = (Random.Range(0, 2) == 1) ? -3 : 3;
			ticketRigidbody.AddForce(new Vector2(xDir, yDir), ForceMode2D.Impulse);
			doAddImpulse = false;
		}
	}

	public void StartMoveTicket()
	{
		doAddImpulse = true;
	}

	public void RandomizeStart()
	{
		randomPosition = new Vector2(Random.Range(-700, 700), Random.Range(-250, 250));
		randomRotationZ = Random.Range(0, 360);

		transform.localPosition = randomPosition;
		transform.rotation = Quaternion.Euler(0, 0, randomRotationZ);

		doAddImpulse = true;
	}

	public void TicketTapped()
	{
		if (gameObject.name.Contains("Good"))
		{
			CheckTickets.CheckTicketsReference.IncrementGoodCounter();
		}
		else
		{
			CheckTickets.CheckTicketsReference.IncrementBadCounter();
		}
		gameObject.SetActive(false);
	}
}
