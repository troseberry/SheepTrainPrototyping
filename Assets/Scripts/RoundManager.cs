using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this should exist on the host/server. all playerminigamehanglers should edit this and be edited by this

public class RoundManager : MonoBehaviour 
{
	public static RoundManager RoundManagerReference;

	private float roundTimer = 95f;

	private float generationTimer;
	public static float timeBeforeDeletion = 8f;

    private static MiniGameScript[] minigameScripts;
	private static List<int> inactiveGameIndexes;

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




		DebugPanel.Log("Round Timer:", "Round Logic", roundTimer);

		DebugPanel.Log("Inactive Count: ", "Round Logic", inactiveGameIndexes.Count);

		string inactive = "";
		for (int i = 0; i < inactiveGameIndexes.Count; i ++)
		{
			inactive += (inactiveGameIndexes[i] + ", ");
		}
		DebugPanel.Log("Inactive List: ", "Round Logic",inactive);

		for (int i = 0; i < minigameScripts.Length; i++)
		{
			DebugPanel.Log(minigameScripts[i].gameObject.name, "Mini Game Scripts", " Status: [A]" + minigameScripts[i].isActive + " [BP]" + minigameScripts[i].isBeingPlayed);
		}
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

			Debug.Log("Chosen Index: " + chosenIndex);
			PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[chosenIndex].SetGameActive();

			StartCoroutine(PlayerMiniGameHandler.HandlerReference.GetMiniGameScripts()[chosenIndex].DeleteTask());

			Debug.Log("Chose: " + minigameScripts[chosenIndex].gameObject.name);

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
}