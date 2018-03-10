using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this should exist on the host/server. all playerminigamehandlers should edit this and be edited by this

public class RoundManager : MonoBehaviour 
{
	public static RoundManager RoundManagerReference;

	private float startingCountdown = 5f;
	private float roundTimer = 90f;

	private bool roundHasStarted = false;
	private bool allPlayersReady = false;
	
	public GameObject startRoundButton;
	public Text countdownText;

	public GameObject hostPlayer;

	void Start () 
	{
		RoundManagerReference = this;	
	}
	
	void Update () 
	{
		if (hostPlayer == null)
		{
			hostPlayer = GameObject.FindGameObjectWithTag("Player");
		}
		if (hostPlayer != null) DebugPanel.Log("Ready Count:", "Round Logic", hostPlayer.GetComponent<PlayerReadyHandler>().crossClientReadyCount);

		if (allPlayersReady) 
		{
			startingCountdown = Mathf.Clamp(startingCountdown -= Time.deltaTime, -2f, 5f);
			
			if (startingCountdown >= 0)
			{
				countdownText.enabled = true;
				countdownText.text = Mathf.CeilToInt(startingCountdown).ToString();
			}
			else if (startingCountdown >= -1f && startingCountdown < 0)
			{
				countdownText.text = "GO!";
			}
			else
			{
				countdownText.enabled = false;
			}
		}

		if (startingCountdown <= 0 && allPlayersReady) 
		{
			
			if (!roundHasStarted)
			{
				TaskManager.StartTaskGeneration();
				roundHasStarted = true;
			}
			else if (roundHasStarted && (roundTimer <= 0 || ChaosManager.ReachedMaxChaos()))
			{
				EndRound();
			}
			roundTimer = Mathf.Clamp(roundTimer -= Time.deltaTime, 0f, 90f);
		}

		TaskManager.CancelTasksDuringRound();

		#region DEBUG
		DebugPanel.Log("Countdown Timer:", "Round Logic", startingCountdown);
		DebugPanel.Log("Round Timer:", "Round Logic", roundTimer);
		DebugPanel.Log("Round Has Started:", "Round Logic", roundHasStarted);
		DebugPanel.Log("All Players Ready:", "Round Logic", allPlayersReady);
		#endregion
	}


	public void StartRound()
	{
		startRoundButton.SetActive(false);
		ChaosManager.SetChaosValue(0);
		allPlayersReady = true;
	}

	public void EndRound()
	{
		startRoundButton.SetActive(true);
		allPlayersReady = false;

		TaskManager.CancelTasksAfterRound();
		TaskManager.StopTaskGeneration();

		roundHasStarted = false;
		startingCountdown = 5f;
		roundTimer = 90f;		
	}

	public static float GetRoundTimer() { return RoundManagerReference.roundTimer; }
}