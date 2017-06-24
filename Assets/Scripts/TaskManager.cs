using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Rename to TimerManager
public class TaskManager : MonoBehaviour 
{
	float timer;
	Text taskTimerText;
	String formattedTimer;
	

	void Start () 
	{
		taskTimerText = transform.Find("TaskTimer").GetComponent<Text>();
		timer = 5f;
	}
	
	void Update () 
	{
		if (timer > 0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			timer = 0;
		}
		formattedTimer = string.Format("{0:00.00}", timer % 60);
		taskTimerText.text = formattedTimer;

		DebugPanel.Log("Timer: ", timer);
	}
}
