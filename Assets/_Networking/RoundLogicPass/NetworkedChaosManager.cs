using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedChaosManager : MonoBehaviour 
{
	private static int chaosValue = 0;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	public static void SetChaosValue(int value) { chaosValue = value; }

	public static bool ReachedMaxChaos() { return chaosValue >= 100; }
}