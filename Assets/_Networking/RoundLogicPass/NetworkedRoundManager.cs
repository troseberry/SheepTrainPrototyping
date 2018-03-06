using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkedRoundManager : NetworkBehaviour 
{
	// public static NetworkedRoundManager NRM;

	private static float startingCountdown = 5f;
	private static float roundTimer = 90f;

	private bool roundHasStarted = false;
	private bool allPlayersReady = false;

	public GameObject readyStatusButton;

	public string readyStatusString = "Unready";
	public Text readyStatusText;
	public bool readyStatus = false;

	public bool otherPlayerStatus = false;

	public static int readyCount = 0;
	// public static int testVar = 0;


	void Start () 
	{
		// NRM = this;
		
	}

	public void AssignReadyButton(GameObject button)
	{
		readyStatusButton = button;
	}
	
	void Update () 
	{
		DebugPanel.Log("Ready Count:", "Round Logic", readyCount);

		//only checking the server's version of these variables
		if (readyCount == 2)
		{
			//start round
			Debug.Log("All Players Ready");
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			// CmdToggleReadyStatus();
			LocalToggleReadyStatus();
		}
	}


	void LocalToggleReadyStatus()
	{
		if (isLocalPlayer)
		{
			readyStatus = !readyStatus;
			readyStatusString = readyStatus ? "Ready" : "Unready";
			readyStatusButton.GetComponentInChildren<Text>().text = readyStatusString;
			readyCount += readyStatus ? 1 : -1;

			// testVar = readyCount;
			Debug.Log("Local Ready Count: " + readyCount);
			// Debug.Log("Local Test: " + testVar);
		}
		// Debug.Log("Global Test: " + testVar);
		UpdateReadyCount(readyCount);
	}

	//think this should be a command
	// [Command]
	// void CmdToggleReadyStatus(int countUpdate)
	// {
	// 	if (isLocalPlayer)
	// 	{
	// 		readyStatus = !readyStatus;
	// 		readyStatusString = readyStatus ? "Ready" : "Unready";
	// 		readyStatusButton.GetComponentInChildren<Text>().text = readyStatusString;
	// 		readyCount += readyStatus ? 1 : -1;
	// 		Debug.Log("Ready Status Changed. New Count: " + readyCount);

	// 		UpdateReadyCount(readyCount);
	// 	}

	// 	// Debug.Log("Client Count Update: " +readyCount);
	// 	// RpcUpdateReadyCount(readyCount);


	// 	//call command to update status on server
	// }

	[Server]
	void UpdateReadyCount(int newCount)
	{
		readyCount = newCount;
		Debug.Log("Client Count Updated: " + newCount);
	}




















	// should be a client rpc
	void StartRound()
	{
		if (isLocalPlayer)
		{
			readyStatusButton.SetActive(false);
			// CmdSetReadyStatus(readyStatusString);
			Debug.Log("Executed on Client");
		}
		//allPlayersReady = true;
	}

	//client rpc
	void EndRound()
	{
		readyStatusButton.SetActive(true);
		allPlayersReady = false;

		// TaskManager.CancelTasksAfterRound();
		// TaskManager.StopTaskGeneration();
		// ChaosManager.SetChaosValue(0);

		roundHasStarted = false;
		startingCountdown = 5f;
		roundTimer = 90f;
	}


	public static float GetRoundTimer() { return roundTimer; }




	// public void ChangeReadyStatus(string status)
	// {
	// 	if (isLocalPlayer)
	// 	{
	// 		readyStatusString = status;
	// 		CmdSetReadyStatus(readyStatusString);
	// 	}
	// }

	// [Command]
	// public void CmdSetReadyStatus(string stauts)
	// {
	// 	Debug.Log("Called Server Command");
	// 	readyStatusString = stauts;
	// }
}