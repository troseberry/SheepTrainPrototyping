using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepArmBehavior : MonoBehaviour 
{
	private bool doMove =  true;
	
	void Update () 
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			if (doMove)
			{
				transform.localPosition = new Vector2(transform.localPosition.x - 500, transform.localPosition.y);
				doMove = false;
			}
		}

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !doMove)
		{
			doMove = true;
		}
	}
}