using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedRoundManager : NetworkBehaviour 
{
	private static float startingCountdown = 5f;
	private static float roundTimer = 90f;

	private bool roundHasStarted = false;
	private bool allPlayersReady = false;

	public GameObject startRoundButton;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	public void StartRound()
	{
		startRoundButton.SetActive(false);
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
}