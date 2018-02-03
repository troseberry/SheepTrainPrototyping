using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestBehavior : MonoBehaviour 
{

	GameObject daytime;
	GameObject nighttime;

	void Start () 
	{
		daytime = transform.GetChild(0).gameObject;
		nighttime = transform.GetChild(1).gameObject;
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
