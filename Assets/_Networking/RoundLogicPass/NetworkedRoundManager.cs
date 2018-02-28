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
	private bool allPlayersReady = false;

	public GameObject startRoundButton;

	[SyncVar]
	public string readyStatus = "Not Ready";
	public TextAlignment readyStatusText;

	void Start () 
	{
		NRM = this;
	}
	
	void Update () 
	{
		
	}

	public void StartRound()
	{
		if (isLocalPlayer)
		{
			startRoundButton.SetActive(false);
			readyStatus = "Ready";
			CmdSetReadyStatus(readyStatus);
			Debug.Log("Executed on Client");
		}
		//allPlayersReady = true;
	}

	public void EndRound()
	{
		startRoundButton.SetActive(true);
		allPlayersReady = false;

		// TaskManager.CancelTasksAfterRound();
		// TaskManager.StopTaskGeneration();
		// ChaosManager.SetChaosValue(0);

		roundHasStarted = false;
		startingCountdown = 5f;
		roundTimer = 90f;
	}

	public static float GetRoundTimer() { return roundTimer; }

	public void ChangeReadyStatus(string status)
	{
		if (isLocalPlayer)
		{
			readyStatus = status;
			CmdSetReadyStatus(readyStatus);
		}
	}

	[Command]
	public void CmdSetReadyStatus(string stauts)
	{
		Debug.Log("Called Server Command");
		readyStatus = stauts;
	}
}