// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;

// public class ChaosManager : MonoBehaviour 
// {
// 	public static ChaosManager ChaosManagerReference; 

// 	private static int chaosValue = 0;
// 	private static int failureValue = 20;
// 	private static int successValue = -5;

// 	public GameObject hostPlayer;
// 	public GameObject localPlayer;

// 	void Start () 
// 	{
// 		ChaosManagerReference = this;
// 	}
	
// 	void Update () 
// 	{
// 		DebugPanel.Log("Chaos: ", "Round Logic", chaosValue);
// 		DebugPanel.Log("Max Chaos Reached: ", "Round Logic", ChaosManager.ReachedMaxChaos());

// 		if (hostPlayer == null)
// 		{
// 			hostPlayer = GameObject.FindGameObjectWithTag("Player");
// 		}
// 		else
// 		{
// 			// chaosValue = hostPlayer.GetComponent<NetworkedChaosManager>().chaosValue;
// 		}

// 		if (localPlayer == null)
// 		{
// 			GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
// 			for (int i = 0; i < allPlayers.Length; i++)
// 			{
// 				if (allPlayers[i].GetComponent<NetworkIdentity>().isLocalPlayer)
// 				{
// 					localPlayer = allPlayers[i];
// 					break;
// 				}
// 			}
// 		}
// 	}

// 	public static void PassedTask()
// 	{
// 		chaosValue = Mathf.Clamp(chaosValue += successValue, 0, 100);
// 		NetworkedChaosManager.ChaosManagerReference.UpdateChaosValue(chaosValue);
		
// 		// ChaosManagerReference.localPlayer.GetComponent<NetworkedChaosManager>().UpdateChaosValue(chaosValue);

// 		// chaosValue = ChaosManagerReference.hostPlayer.GetComponent<NetworkedChaosManager>().chaosValue;

// 		// Debug.Log("Chaos: " + chaosValue);
// 	}

// 	public static void FailedTask()
// 	{
// 		chaosValue = Mathf.Clamp(chaosValue += failureValue, 0, 100);
// 		NetworkedChaosManager.ChaosManagerReference.UpdateChaosValue(chaosValue);

// 		// ChaosManagerReference.localPlayer.GetComponent<NetworkedChaosManager>().UpdateChaosValue(chaosValue);
// 	}

// 	public static void ResetChaos() { chaosValue = 0; }

// 	public static void SetChaosValue(int value)
// 	{
// 		chaosValue = value;
// 		NetworkedChaosManager.ChaosManagerReference.UpdateChaosValue(chaosValue);

// 		// ChaosManagerReference.localPlayer.GetComponent<NetworkedChaosManager>().UpdateChaosValue(chaosValue);

// 		// chaosValue = ChaosManagerReference.hostPlayer.GetComponent<NetworkedChaosManager>().chaosValue;
// 	}

// 	public static int GetChaosValue() { return chaosValue; }

// 	public static bool ReachedMaxChaos() { return chaosValue >= 100; }
// }