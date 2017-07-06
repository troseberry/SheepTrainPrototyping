using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestBehavior : MonoBehaviour 
{

	void Start () 
	{
		
	}
	
	void Update () 
	{
		if (transform.localPosition.x == 0)
		{
			//check if did shake if daytime
			//check that did not shake if nighttime
			//if either one fails, failedEarly = true
		}
	}
}
