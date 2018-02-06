using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosManager : MonoBehaviour 
{
	private static int chaosValue = 80;
	private static int failureValue = 20;
	private static int successValue = -5;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		DebugPanel.Log("Chaos: ", "Round Logic", chaosValue);
		DebugPanel.Log("Max Chaos Reached: ", "Round Logic", ChaosManager.ReachedMaxChaos());
	}

	public static void PassedTask()
	{
		chaosValue = Mathf.Clamp(chaosValue += successValue, 0, 100);
	}

	public static void FailedTask()
	{
		chaosValue = Mathf.Clamp(chaosValue += failureValue, 0, 100);
	}

	public static void ResetChaos() { chaosValue = 0; }

	public static void SetChaosValue(int value) { chaosValue = value; }

	public static bool ReachedMaxChaos() { return chaosValue >= 100; }
}