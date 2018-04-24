// DOn't need this. Regular round manager should be okay I think. It'll be able to call functions
// on networked objs. I don't think any functionality within RoundManager requires it to inherit
// from NetworkBehavior. But I could be wrong. Will investigate further...

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkedRoundManager : NetworkBehaviour 
{
	public static NetworkedRoundManager NRM;

	private static float startingCountdown = 5f;
	private static float roundTimer = 5f;
	private static float compTimer = 5f;

	private static bool roundHasStarted = false;
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
		DebugPanel.Log("Round Timer:", "Round Logic", roundTimer);
		DebugPanel.Log("Comp Timer:", "Round Logic", compTimer);
		DebugPanel.Log("Round Has Started:", "Round Logic", roundHasStarted);
		DebugPanel.Log("All Players Ready:", "Round Logic", allPlayersReady);

		if (readyCount == 3 && !allPlayersReady)
		{
			StartRound();
		}

		if (allPlayersReady)
		{
			if (isLocalPlayer) startingCountdown = Mathf.Clamp(startingCountdown -= Time.deltaTime, -2f, 5f);
		}

		if (startingCountdown <= 0 && allPlayersReady)
		{
			if (!roundHasStarted)
			{
				NetworkedTaskManager.StartTaskGeneration();
				roundHasStarted = true;
			}
			else if (roundHasStarted && (roundTimer <= 0) || NetworkedChaosManager.ChaosManagerReference.ReachedMaxChaos())
			{
				if (isServer) RpcEndRound();
			}
			if (isLocalPlayer) roundTimer = Mathf.Clamp(roundTimer -= Time.deltaTime, 0f, 90f);
			compTimer = Mathf.Clamp(compTimer -= Time.deltaTime, 0f, 90f);
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
		Debug.Log("All Players Ready");
		allPlayersReady = true;
		NetworkedChaosManager.ChaosManagerReference.SetChaosValue(0);
	}

	void EndRound()
	{
		// Debug.Log("End Round. Local: " + isLocalPlayer);
		
		allPlayersReady = false;
		readyCount = 0;
		readyStatus = false;

		// TaskManager.CancelTasksAfterRound();
		// TaskManager.StopTaskGeneration();
		// ChaosManager.SetChaosValue(0);

		roundHasStarted = false;
		startingCountdown = 5f;
		roundTimer = 5f;
	}

	[ClientRpc]
	void RpcEndRound()
	{
		// Debug.Log("End Round. Local: " + isLocalPlayer);
		
		allPlayersReady = false;
		readyCount = 0;
		readyStatus = false;

		// GetComponent<PlayerReadyHandler>().ResetPlayerRoundItems();

		// TaskManager.CancelTasksAfterRound();
		// TaskManager.StopTaskGeneration();
		// ChaosManager.SetChaosValue(0);

		roundHasStarted = false;
		startingCountdown = 5f;
		roundTimer = 5f;
	}

	public static float GetRoundTimer() { return roundTimer; }

	public static float GetStartCountdown() { return startingCountdown; }

	public static bool AreAllPlayersReady() { return allPlayersReady; }
}