using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosManager : MonoBehaviour 
{
	private static int chaosValue = 0;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		DebugPanel.Log("Chaos: ", "Round Logic", chaosValue);
	}

	public static void PassedTask()
	{
		chaosValue = Mathf.Clamp(chaosValue -= 5, 0, 100);
	}

	public static void FailedTask()
	{
		chaosValue = Mathf.Clamp(chaosValue += 20, 0, 100);
	}

	public static void ResetChaos() { chaosValue = 0; }
}