﻿// Each client has access to the same sync var because this script is active on
// on players, both the local and remote versions on all clients.
// Where as the PlayerReadyHandler is only active on the local player version

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkedRoundManager : NetworkBehaviour 
{
	public static NetworkedRoundManager NRM;

	private static float startingCountdown = 5f;
	private static float roundTimer = 90f;

	private bool roundHasStarted = false;
	private static bool allPlayersReady = false;

	[SyncVar]
	public int readyCount = 0;
	public bool readyStatus = false;

	
	void Start()
	{
		NRM = this;
	}

	void OnEnable()
	{
		NRM = this;
	}

	void Update ()
	{
		DebugPanel.Log("Ready Count:", "Round Logic", readyCount);
		DebugPanel.Log("Countdown Timer:", "Round Logic", startingCountdown);

		if (readyCount == 2 && !allPlayersReady)
		{
			//start round
			Debug.Log("All Players Ready");
			allPlayersReady = true;
		}

		if (allPlayersReady)
		{
			if (isLocalPlayer) startingCountdown = Mathf.Clamp(startingCountdown -= Time.deltaTime, -2f, 5f);
		}
	}


	public void UpdateGroupReadyCount()
	{
		readyStatus = !readyStatus;

		readyCount += readyStatus ? 1 : -1;
		CmdUpdateGroupReadyCount(readyCount);
	}

	[Command]
	void CmdUpdateGroupReadyCount(int count)
	{
		readyCount = count;

		RpcUpdateGroupReadyCount(readyCount);
	}

	[ClientRpc]
	void RpcUpdateGroupReadyCount(int newCount)
	{
		readyCount = newCount;
	}






	// should be a client rpc
	void StartRound()
	{
		if (isLocalPlayer)
		{
			// readyStatusButton.SetActive(false);
			// CmdSetReadyStatus(readyStatusString);
			Debug.Log("Executed on Client");
		}
		//allPlayersReady = true;
	}

	//client rpc
	void EndRound()
	{
		// readyStatusButton.SetActive(true);
		allPlayersReady = false;

		// TaskManager.CancelTasksAfterRound();
		// TaskManager.StopTaskGeneration();
		// ChaosManager.SetChaosValue(0);

		roundHasStarted = false;
		startingCountdown = 5f;
		roundTimer = 90f;
	}

	public static float GetRoundTimer() { return roundTimer; }

	public static float GetStartCountdown() { return startingCountdown; }

	public static bool AreAllPlayersReady() { return allPlayersReady; }
}