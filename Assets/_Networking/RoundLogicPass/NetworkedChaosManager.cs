using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkedChaosManager : NetworkBehaviour 
{
	public static NetworkedChaosManager ChaosManagerReference;

	public int chaosValue = 0;

	private int failureValue = 20;
	private int successValue = -5;

	void Start () 
	{
		ChaosManagerReference = this;
	}
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			FailedTask();
		}

		if (Input.GetKeyDown(KeyCode.G))
		{
			PassedTask();
		}
	}

	public void PassedTask()
	{
		chaosValue = Mathf.Clamp(chaosValue += successValue, 0, 100);
		UpdateChaosValue(chaosValue);
	}

	public void FailedTask()
	{
		chaosValue = Mathf.Clamp(chaosValue += failureValue, 0, 100);
		UpdateChaosValue(chaosValue);
	}

	public void ResetChaos()
	{
		chaosValue = 0;
		UpdateChaosValue(chaosValue);
	}

	public void SetChaosValue(int value)
	{
		chaosValue = value;
		UpdateChaosValue(chaosValue);
	}

	public int GetChaosValue() { return chaosValue; }

	public bool ReachedMaxChaos() { return chaosValue >= 100; }



	public void UpdateChaosValue(int value)
	{
		chaosValue = value;
		// Debug.Log("Chaos: " + chaosValue + " - " + name);
		CmdUpdateChaosValue(chaosValue);
	}

	[Server]
	public void MissedTaskOnServer()
	{
		chaosValue = Mathf.Clamp(chaosValue += failureValue, 0, 100);
		// Debug.Log("Chaos (SRV): " + chaosValue + " - " + name);
		UpdateChaosValue(chaosValue);
	}

	[Command]
	void CmdUpdateChaosValue(int chaosNumber)
	{
		chaosValue = chaosNumber;
		// Debug.Log("Chaos (CMD): " + chaosValue + " - " + name);
		RpcUpdateChaosValue(chaosValue);
	}

	[ClientRpc]
	void RpcUpdateChaosValue(int newValue)
	{
		chaosValue = newValue;
		// Debug.Log("Chaos (RPC): " + chaosValue + " - " + name);
	}
}