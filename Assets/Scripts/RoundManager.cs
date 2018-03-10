﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this should exist on the host/server. all playerminigamehandlers should edit this and be edited by this

public class RoundManager : MonoBehaviour 
{
	public static RoundManager RoundManagerReference;

	public int maxPlayers;

	public GameObject hostPlayer;

	private static float startingCountdown = 5f;
	private static float roundTimer = 90f;

	private static bool roundHasStarted = false;
	private static bool allPlayersReady = false;
	private int readyCount = 0;


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
		else
		{
			readyCount = hostPlayer.GetComponent<PlayerReadyHandler>().crossClientReadyCount;
		}

		if (readyCount > 0 && readyCount == OverriddenNetworkManager.GetPlayerCount() && !allPlayersReady)
		{
			StartRound();
		}


		if (allPlayersReady) 
		{
			startingCountdown = Mathf.Clamp(startingCountdown -= Time.deltaTime, -2f, 5f);

			 
			if (startingCountdown <= 0) 
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
		}

		TaskManager.CancelTasksDuringRound();

		

		#region DEBUG
		DebugPanel.Log("Countdown Timer:", "Round Logic", startingCountdown);
		DebugPanel.Log("Round Timer:", "Round Logic", roundTimer);
		DebugPanel.Log("Round Has Started:", "Round Logic", roundHasStarted);
		DebugPanel.Log("All Players Ready:", "Round Logic", allPlayersReady);

		if (hostPlayer != null) DebugPanel.Log("Ready Count:", "Round Logic", hostPlayer.GetComponent<PlayerReadyHandler>().crossClientReadyCount);
		#endregion
	}


	public void StartRound()
	{
		// startRoundButton.SetActive(false);
		ChaosManager.SetChaosValue(0);
		roundTimer = 90f;
		allPlayersReady = true;
	}

	public void EndRound()
	{
		// startRoundButton.SetActive(true);
		allPlayersReady = false;
		readyCount = 0;	

		TaskManager.CancelTasksAfterRound();
		TaskManager.StopTaskGeneration();

		roundHasStarted = false;
		startingCountdown = 5f;
	}

	public static float GetRoundTimer() { return roundTimer; }

	public static float GetStartCountdown() { return startingCountdown; }

	public static bool AreAllPlayersReady() { return allPlayersReady; }

	public static bool HasRoundStarted() { return roundHasStarted; }
}