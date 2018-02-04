﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTickets : MiniGameScript 
{
	public static CheckTickets CheckTicketsReference;
	public static float timeLimit = 5.0f;

	public Transform ticketObjects;

	bool isComplete = false;
	bool failedEarly = false;
	bool doStartGame = true;

	int goodCounter;
	int badCounter;

	void Start () 
	{
		CheckTicketsReference = this;
	}
	
	void Update () 
	{
		if (!isComplete && PlayerMiniGameHandler.IsInGame() && doStartGame)
		{
			doStartGame = false;
		}

		if (PlayerMiniGameHandler.timer > 0)
		{
			isComplete = (goodCounter == 3);
			failedEarly = (badCounter >= 3);
		}

		if (isComplete || failedEarly || PlayerMiniGameHandler.timer == 0) EndGame();
	}

	public void IncrementGoodCounter()
	{
		goodCounter++;
	}

	public void IncrementBadCounter()
	{
		badCounter++;
	}

	void EndGame()
	{
		PlayerMiniGameHandler.EndMiniGame(((failedEarly) ? false : isComplete));
	}

	public override void ResetGame()
	{
		goodCounter = 0;
		badCounter = 0;

		for (int i = 0; i < ticketObjects.childCount; i++)
		{
			ticketObjects.GetChild(i).gameObject.SetActive(true);
			ticketObjects.GetChild(i).gameObject.GetComponent<TicketBehavior>().StartMoveTicket();
		}

		isComplete = false;
		failedEarly = false;
		doStartGame = true;
	}

	public override float GetTimeLimit() { return timeLimit; }
}