using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepWool : MiniGameScript 
{
	public static SweepWool SweepWoolReference;
	public static float timeLimit = 3f;
	private bool isComplete;

	public GameObject[] woolObjects;
	private static int sweptCount = 0;

	private bool doStartGame = true;

	void Start () 
	{
		SweepWoolReference = this;
	}
	
	void Update () 
	{
		if (!isComplete && MiniGameManager.ManagerReference.IsInGame() && doStartGame)
		{
			doStartGame = false;
		}
		
		if (isComplete || MiniGameManager.ManagerReference.timer == 0) EndGame();

		if (MiniGameManager.ManagerReference.timer > 0) isComplete = (sweptCount == woolObjects.Length);
	}

	public static void IncrementSweptCount() { sweptCount ++; }
	public static int GetSweptCount() { return sweptCount; }

	void EndGame()
	{
		// Debug.Log("Swept Count: " + sweptCount);
		// Debug.Log("Wool Objs: " + woolObjects.Length);
		// Debug.Log("Equal: " + (sweptCount == woolObjects.Length));

		MiniGameManager.ManagerReference.didWin = isComplete;
		MiniGameManager.ManagerReference.EndMiniGame();
	}

	public override void ResetGame()
	{
		sweptCount = 0;

		//reset position of wool 
		for (int i = 0; i < woolObjects.Length; i++)
		{
			woolObjects[i].GetComponent<WoolBehavior>().ResetPosition();
		}
		
		isComplete = false;
		doStartGame = true;
	}
}