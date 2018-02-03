using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceCollisionCheck : MonoBehaviour 
{
	private bool coalIsPassing;
	private bool wasActivated;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		if (!coalIsPassing) wasActivated = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == 8)
        {
			wasActivated = true;
			coalIsPassing = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.layer == 8)
        {
			coalIsPassing = false;
		}
	}

	public bool WasActivated() { return wasActivated; }
}