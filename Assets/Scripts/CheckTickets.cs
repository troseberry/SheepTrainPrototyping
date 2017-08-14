using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTickets : MonoBehaviour 
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
		if (!isComplete && MiniGameManager.ManagerReference.IsInGame() && doStartGame)
		{
			// for (int i = 0; i < 16; i++)
			// {
			// 	TicketBehavior currentTicker = ticketObjects.GetChild(i).gameObject.GetComponent<TicketBehavior>();
			// 	currentTicker.RandomizeStart();
			// 	currentTicker.StartMoveTicket();
			// }
			doStartGame = false;
		}

		if (MiniGameManager.ManagerReference.timer > 0)
		{
			isComplete = (goodCounter == 3);
			failedEarly = (badCounter >= 3);
		}

		if (isComplete || failedEarly || MiniGameManager.ManagerReference.timer == 0) EndGame();
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
		MiniGameManager.ManagerReference.didWin = (failedEarly) ? failedEarly : isComplete;
		MiniGameManager.ManagerReference.EndMiniGame();
	}

	public void ResetGame()
	{
		goodCounter = 0;
		badCounter = 0;

		for (int i = 0; i < 16; i++)
		{
			ticketObjects.GetChild(i).gameObject.SetActive(true);
		}

		isComplete = false;
		failedEarly = false;
		doStartGame = true;
	}
}
