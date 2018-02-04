using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this should exist on the host/server. all playerminigamehandlers should edit this and be edited by this

public class RoundManager : MonoBehaviour 
{
	public static RoundManager RoundManagerReference;

	private float roundTimer = 95f;

	private float generationTimer;
	public static float timeBeforeDeletion = 8f;

    private static MiniGameScript[] minigameScripts;
	private static List<int> inactiveGameIndexes;
	private static List<int> activeGameIndexes = new List<int>();

	private bool roundHasStarted = false;

	void Start () 
	{
		RoundManagerReference = this;
		
	}
	
	void Update () 
	{
		if (roundTimer > 0)
		{
			roundTimer -= Time.deltaTime;
		}
		else if (roundTimer <= 0)
		{
			roundTimer = 0;
		}

		if (roundTimer <= 90 && !roundHasStarted)
		{
			roundHasStarted = true;
			StartCoroutine("GenerateTask");
		}


		for (int i = 0; i < activeGameIndexes.Count; i++)
		{
			if (minigameScripts[activeGameIndexes[i]].isBeingPlayed && minigameScripts[activeGameIndexes[i]].awaitingDeletion)
			{
				InterruptDeletion(activeGameIndexes[i]);
			}
		}




		#region DEBUG
		DebugPanel.Log("Round Timer:", "Round Logic", roundTimer);

		DebugPanel.Log("Inactive Count: ", "Round Logic", inactiveGameIndexes.Count);

		string inactive = "";
		for (int j = 0; j < inactiveGameIndexes.Count; j ++)
		{
			inactive += (inactiveGameIndexes[j] + ", ");
		}
		DebugPanel.Log("Inactive List: ", "Round Logic", inactive);

		string active = "";
		for (int l = 0; l < activeGameIndexes.Count; l ++)
		{
			active += (activeGameIndexes[l] + ", ");
		}
		DebugPanel.Log("Active List: ", "Round Logic", active);

		for (int k = 0; k < minigameScripts.Length; k++)
		{
			DebugPanel.Log(minigameScripts[k].gameObject.name, "Mini Game Scripts", " Status: [A]" + minigameScripts[k].isActive + " [BP]" + minigameScripts[k].isBeingPlayed + " [AD]: " + minigameScripts[k].awaitingDeletion);
		}
		#endregion
	}


	public void StartRound()
	{
		roundTimer = 90f;
	}

	public void EndRound()
	{

	}

	IEnumerator GenerateTask()
	{
		while (roundTimer > 0)
		{
			ChooseTask();
			yield return new WaitForSeconds(5f);
		}
	}

	void ChooseTask()
	{
		if (inactiveGameIndexes.Count > 0)
		{
			int chosenIndex = inactiveGameIndexes[Random.Range(0, inactiveGameIndexes.Count)];

			Debug.Log("Chose: [" + chosenIndex + "] " + minigameScripts[chosenIndex].gameObject.name);

			PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[chosenIndex].SetGameActive();

			if (!activeGameIndexes.Contains(chosenIndex)) activeGameIndexes.Add(chosenIndex);

			InitiateDeletion(chosenIndex);

			inactiveGameIndexes.Remove(chosenIndex);
		}
		else
		{
			//end round
			StopCoroutine("GenerateTask");
			Debug.Log("Stopped Generating. Empty List");
		}
	}



	public static void SetMiniGameScripts(MiniGameScript[] scripts)
	{
		minigameScripts = scripts;
		inactiveGameIndexes = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14};
	}

	public static void SetMiniGameStatusInactive(int gameIndex)
	{
		if (activeGameIndexes.Contains(gameIndex)) activeGameIndexes.Remove(gameIndex);
		
		if (!inactiveGameIndexes.Contains(gameIndex))
		{
			inactiveGameIndexes.Add(gameIndex);
			Debug.Log(minigameScripts[gameIndex].gameObject.name + " Added Back: " + gameIndex);
		}
		else
		{
			Debug.Log("List already contains index: " + gameIndex);
		}
	}

	void InitiateDeletion(int gameIndex)
	{
		switch (gameIndex)
		{
			case 0:
				StartCoroutine("DeleteSpeedLevers");
				break;
			case 1:
				StartCoroutine("DeletePressureValve");
				break;
			case 2:
				StartCoroutine("DeleteFlickFuel");
				break;
			case 3:
				StartCoroutine("DeleteSoupFly");
				break;
			case 4:
				StartCoroutine("DeleteClearTable");
				break;
			case 5:
				StartCoroutine("DeleteServeTea");
				break;
			case 6:
				StartCoroutine("DeleteWoolCuts");
				break;
			case 7:
				StartCoroutine("DeleteMustacheRoll");
				break;
			case 8:
				StartCoroutine("DeleteSweepWool");
				break;
			case 9:
				StartCoroutine("DeleteSheepJump");
				break;
			case 10:
				StartCoroutine("DeleteWakeGuests");
				break;
			case 11:
				StartCoroutine("DeleteMakeBeds");
				break;
			case 12:
				StartCoroutine("DeleteTakeInventory");
				break;
			case 13:
				StartCoroutine("DeleteCheckTickets");
				break;
			case 14:
				StartCoroutine("DeleteSaveSheep");
				break;
		}
	}

	void InterruptDeletion(int gameIndex)
	{
		Debug.Log("Interrupting Coroutine: " + gameIndex);
		switch (gameIndex)
		{
			case 0:
				StopCoroutine("DeleteSpeedLevers");
				break;
			case 1:
				StopCoroutine("DeletePressureValve");
				break;
			case 2:
				StopCoroutine("DeleteFlickFuel");
				break;
			case 3:
				StopCoroutine("DeleteSoupFly");
				break;
			case 4:
				StopCoroutine("DeleteClearTable");
				break;
			case 5:
				StopCoroutine("DeleteServeTea");
				break;
			case 6:
				StopCoroutine("DeleteWoolCuts");
				break;
			case 7:
				StopCoroutine("DeleteMustacheRoll");
				break;
			case 8:
				StopCoroutine("DeleteSweepWool");
				break;
			case 9:
				StopCoroutine("DeleteSheepJump");
				break;
			case 10:
				StopCoroutine("DeleteWakeGuests");
				break;
			case 11:
				StopCoroutine("DeleteMakeBeds");
				break;
			case 12:
				StopCoroutine("DeleteTakeInventory");
				break;
			case 13:
				StopCoroutine("DeleteCheckTickets");
				break;
			case 14:
				StopCoroutine("DeleteSaveSheep");
				break;
		}
	}

	#region DELETION IENUMERATORS
	//Try making an internal ienumerator to call the external one (from individual mini game scripts) so that the internal one can at least be interrupted properly
	IEnumerator DeleteSpeedLevers()
	{
		Debug.Log("Speed Levers Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[0].SetGameInactive();
		SetMiniGameStatusInactive(0);
		Debug.Log("Deleted Speed Levers");
	}

	IEnumerator DeletePressureValve()
	{
		Debug.Log("Pressure Valve Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[1].SetGameInactive();
		SetMiniGameStatusInactive(1);
		Debug.Log("Deleted Pressure Valve");
	}

	IEnumerator DeleteFlickFuel()
	{
		Debug.Log("Flick Fuel Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[2].SetGameInactive();
		SetMiniGameStatusInactive(2);
		Debug.Log("Deleted Flick Fuel");
	}

	IEnumerator DeleteSoupFly()
	{
		Debug.Log("Soup Fly Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[3].SetGameInactive();
		SetMiniGameStatusInactive(3);
		Debug.Log("Deleted Soup Fly");
	}

	IEnumerator DeleteClearTable()
	{
		Debug.Log("Clear Table Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[4].SetGameInactive();
		SetMiniGameStatusInactive(4);
		Debug.Log("Deleted Clear Table");
	}

	IEnumerator DeleteServeTea()
	{
		Debug.Log("Serve Tea Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[5].SetGameInactive();
		SetMiniGameStatusInactive(5);
		Debug.Log("Deleted Serve Tea");
	}

	IEnumerator DeleteWoolCuts()
	{
		Debug.Log("Wool Cuts Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[6].SetGameInactive();
		SetMiniGameStatusInactive(6);
		Debug.Log("Deleted Wool Cuts");
	}

	IEnumerator DeleteMustacheRoll()
	{
		Debug.Log("Mustache Roll Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[7].SetGameInactive();
		SetMiniGameStatusInactive(7);
		Debug.Log("Deleted Mustache Roll");
	}

	IEnumerator DeleteSweepWool()
	{
		Debug.Log("Sweep Wool Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[8].SetGameInactive();
		SetMiniGameStatusInactive(8);
		Debug.Log("Deleted Sweep Wool");
	}

	IEnumerator DeleteSheepJump()
	{
		Debug.Log("Sheep Jump Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[9].SetGameInactive();
		SetMiniGameStatusInactive(9);
		Debug.Log("Deleted Sheep Jump");
	}

	IEnumerator DeleteWakeGuests()
	{
		Debug.Log("Wake Guests Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[10].SetGameInactive();
		SetMiniGameStatusInactive(10);
		Debug.Log("Deleted Wake Guests");
	}

	IEnumerator DeleteMakeBeds()
	{
		Debug.Log("Make Beds Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[11].SetGameInactive();
		SetMiniGameStatusInactive(11);
		Debug.Log("Deleted Make Beds");
	}

	IEnumerator DeleteTakeInventory()
	{
		Debug.Log("Take Inventory Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[12].SetGameInactive();
		SetMiniGameStatusInactive(12);
		Debug.Log("Deleted Take Inventory");
	}

	IEnumerator DeleteCheckTickets()
	{
		Debug.Log("Check Tickets Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[13].SetGameInactive();
		SetMiniGameStatusInactive(13);
		Debug.Log("Deleted Check Tickets");
	}

	IEnumerator DeleteSaveSheep()
	{
		Debug.Log("Save Sheep Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");

		yield return new WaitForSeconds(timeBeforeDeletion);
		
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[14].SetGameInactive();
		SetMiniGameStatusInactive(14);
		Debug.Log("Deleted Save Sheep");
	}
	#endregion
}