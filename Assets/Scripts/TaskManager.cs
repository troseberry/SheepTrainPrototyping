using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour 
{
	public static TaskManager TaskManagerReference;
	
	private static MiniGameScript[] taskScripts;
	private static List<int> inactiveGameIndexes = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14};
	private static List<int> activeGameIndexes = new List<int>();
	
	public static float generationFrequency = 5f;
	public static float deletionFrequency = 8f;

	void Start () 
	{
		TaskManagerReference = this;
	}
	
	void Update () 
	{
		#region DEBUG
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

		for (int k = 0; k < taskScripts.Length; k++)
		{
			DebugPanel.Log(taskScripts[k].gameObject.name, "Mini Game Scripts", " Status: [A]" + taskScripts[k].isActive + " [BP]" + taskScripts[k].isBeingPlayed + " [AD]: " + taskScripts[k].awaitingDeletion);
		}
		#endregion
	}

	public static void SetTaskScripts(MiniGameScript[] scripts) { taskScripts = scripts; }

	public static void SetMiniGameStatusInactive(int gameIndex)
	{
		if (activeGameIndexes.Contains(gameIndex)) activeGameIndexes.Remove(gameIndex);
		
		if (!inactiveGameIndexes.Contains(gameIndex))
		{
			inactiveGameIndexes.Add(gameIndex);
			Debug.Log(taskScripts[gameIndex].gameObject.name + " Added Back: " + gameIndex);

			if (!CarHasActiveTasks(gameIndex)) MiniMap.HideNotification(Mathf.CeilToInt(gameIndex / 3));
		}
		else
		{
			Debug.Log("List already contains index: " + gameIndex);
		}
	}


	public static void StartTaskGeneration()
	{
		TaskManagerReference.StartCoroutine("GenerateTask");
	}

	public static void StopTaskGeneration()
	{
		TaskManagerReference.StopCoroutine("GenerateTask");
	}

	IEnumerator GenerateTask()
	{
		while (RoundManager.GetRoundTimer() > 0 && inactiveGameIndexes.Count > 0)
		{
			ChooseTask();
			yield return new WaitForSeconds(generationFrequency);
		}
	}
	
	void ChooseTask()
	{
		// int chosenIndex = inactiveGameIndexes[Random.Range(0, inactiveGameIndexes.Count)];

		NetworkedTaskManager.TaskManagerReference.GenerateIndex(inactiveGameIndexes);
		int chosenIndex = NetworkedTaskManager.TaskManagerReference.GetGeneratedIndex();

		Debug.Log("Chose: [" + chosenIndex + "] " + taskScripts[chosenIndex].gameObject.name);

		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[chosenIndex].SetGameActive();
		InitiateDeletion(chosenIndex);
		MiniMap.DisplayNotification(Mathf.CeilToInt(chosenIndex / 3));


		if (!activeGameIndexes.Contains(chosenIndex)) activeGameIndexes.Add(chosenIndex);
		inactiveGameIndexes.Remove(chosenIndex);
	}

	public static void CancelTasksDuringRound()
	{
		for (int i = 0; i < activeGameIndexes.Count; i++)
		{
			if (taskScripts[activeGameIndexes[i]].isBeingPlayed && taskScripts[activeGameIndexes[i]].awaitingDeletion)
			{
				TaskManagerReference.InterruptDeletion(activeGameIndexes[i]);
			}
		}
	}

	public static void CancelTasksAfterRound()
	{
		//Doesn't handle closing games that are open for players
		for (int i = 0; i < activeGameIndexes.Count; i++)
		{
			if (taskScripts[activeGameIndexes[i]].awaitingDeletion)
			{
				TaskManagerReference.InterruptDeletion(activeGameIndexes[i]);
				taskScripts[activeGameIndexes[i]].SetGameInactive();
				SetMiniGameStatusInactive(activeGameIndexes[i]);
			}
		}
	}





	static bool CarHasActiveTasks(int taskIndex)
	{
		int starting = Mathf.CeilToInt(taskIndex / 3) * 3;

		return activeGameIndexes.Contains(starting) || activeGameIndexes.Contains(starting + 1) || activeGameIndexes.Contains(starting + 2);
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
		Debug.Log("Speed Levers Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);

		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[0].SetGameInactive();
		SetMiniGameStatusInactive(0);

		Debug.Log("Deleted Speed Levers");
	}

	IEnumerator DeletePressureValve()
	{
		Debug.Log("Pressure Valve Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);
		
		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[1].SetGameInactive();
		SetMiniGameStatusInactive(1);

		Debug.Log("Deleted Pressure Valve");
	}

	IEnumerator DeleteFlickFuel()
	{
		Debug.Log("Flick Fuel Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);
		
		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[2].SetGameInactive();
		SetMiniGameStatusInactive(2);

		Debug.Log("Deleted Flick Fuel");
	}

	IEnumerator DeleteSoupFly()
	{
		Debug.Log("Soup Fly Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);
		
		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[3].SetGameInactive();
		SetMiniGameStatusInactive(3);

		Debug.Log("Deleted Soup Fly");
	}

	IEnumerator DeleteClearTable()
	{
		Debug.Log("Clear Table Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);
		
		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[4].SetGameInactive();
		SetMiniGameStatusInactive(4);

		Debug.Log("Deleted Clear Table");
	}

	IEnumerator DeleteServeTea()
	{
		Debug.Log("Serve Tea Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);
		
		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[5].SetGameInactive();
		SetMiniGameStatusInactive(5);

		Debug.Log("Deleted Serve Tea");
	}

	IEnumerator DeleteWoolCuts()
	{
		Debug.Log("Wool Cuts Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);
		
		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[6].SetGameInactive();
		SetMiniGameStatusInactive(6);

		Debug.Log("Deleted Wool Cuts");
	}

	IEnumerator DeleteMustacheRoll()
	{
		Debug.Log("Mustache Roll Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);
		
		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[7].SetGameInactive();
		SetMiniGameStatusInactive(7);

		Debug.Log("Deleted Mustache Roll");
	}

	IEnumerator DeleteSweepWool()
	{
		Debug.Log("Sweep Wool Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);
		
		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[8].SetGameInactive();
		SetMiniGameStatusInactive(8);

		Debug.Log("Deleted Sweep Wool");
	}

	IEnumerator DeleteSheepJump()
	{
		Debug.Log("Sheep Jump Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);
		
		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[9].SetGameInactive();
		SetMiniGameStatusInactive(9);

		Debug.Log("Deleted Sheep Jump");
	}

	IEnumerator DeleteWakeGuests()
	{
		Debug.Log("Wake Guests Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);
		
		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[10].SetGameInactive();
		SetMiniGameStatusInactive(10);

		Debug.Log("Deleted Wake Guests");
	}

	IEnumerator DeleteMakeBeds()
	{
		Debug.Log("Make Beds Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);
		
		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[11].SetGameInactive();
		SetMiniGameStatusInactive(11);

		Debug.Log("Deleted Make Beds");
	}

	IEnumerator DeleteTakeInventory()
	{
		Debug.Log("Take Inventory Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);
		
		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[12].SetGameInactive();
		SetMiniGameStatusInactive(12);

		Debug.Log("Deleted Take Inventory");
	}

	IEnumerator DeleteCheckTickets()
	{
		Debug.Log("Check Tickets Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);
		
		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[13].SetGameInactive();
		SetMiniGameStatusInactive(13);

		Debug.Log("Deleted Check Tickets");
	}

	IEnumerator DeleteSaveSheep()
	{
		Debug.Log("Save Sheep Deletion in " + deletionFrequency + " Seconds...");

		yield return new WaitForSeconds(deletionFrequency);
		
		ChaosManager.FailedTask();
		PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[14].SetGameInactive();
		SetMiniGameStatusInactive(14);
		
		Debug.Log("Deleted Save Sheep");
	}
	#endregion

}	