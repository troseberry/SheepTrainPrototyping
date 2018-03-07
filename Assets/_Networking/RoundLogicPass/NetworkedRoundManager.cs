using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkedRoundManager : NetworkBehaviour 
{
	private static float startingCountdown = 5f;
	private static float roundTimer = 90f;

	private bool roundHasStarted = false;
	private bool allPlayersReady = false;

	[SyncVar]
	public string readyStatusString = "Unready";
	public bool readyStatus = false;
	
	public Text readyStatusButtonText;
	public Text readyStatusPlayerText;


	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			ToggleReady();
		}
	}


	public void ToggleReady()
	{
		readyStatus = !readyStatus;

		readyStatusString = readyStatus ? "Ready" : "Unready";

		readyStatusButtonText.text = readyStatusString;
		readyStatusPlayerText.text = readyStatusString;

		CmdToggleReady(readyStatusString);
	}

	[Command] 
	void CmdToggleReady(string statusString)
	{
		readyStatusString = statusString;
		readyStatusPlayerText.text = readyStatusString;

		RpcUpdateReadyStatus(readyStatusString);
	}

	[ClientRpc]
	void RpcUpdateReadyStatus(string newStatus)
	{
		readyStatusPlayerText.text = newStatus;
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
}