using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTickets : MiniGameScript 
{
	public static CheckTickets CheckTicketsReference;
	public static float timeLimit = 5.0f;

	private bool isComplete = false;
	private bool failedEarly = false;
	private bool doStartGame = true;
    private bool didEndGame = false;

	public Transform ticketObjects;

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

		if ((isComplete || failedEarly || PlayerMiniGameHandler.timer == 0) && !didEndGame)
		{
			EndGame();
			didEndGame = true;
		}
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
    	didEndGame = false;
	}

	public override float GetTimeLimit() { return timeLimit; }
}
