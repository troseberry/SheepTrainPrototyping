using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSheep : MiniGameScript 
{
	public static SaveSheep SaveSheepReference;
	public static float timeLimit = 3f;
	private bool isComplete;

	public Transform sheepArm;
	private bool doMove =  true;
	private int swipeCount = 0;

	bool doStartGame;

	void Start () 
	{
		SaveSheepReference = this;
		doStartGame = true;
	}
	
	void Update ()
	{
		if (!isComplete && MiniGameManager.ManagerReference.IsInGame() && doStartGame)
		{
			// swipeCount = 0;
			// sheepArm.localPosition = new Vector2(1000, 0);
			doStartGame = false;
		}

		// SWIPE INPUT
		// need to determine if delay is better with phase as Began instead of Moved
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			if (doMove)
			{
				sheepArm.localPosition = new Vector2(sheepArm.localPosition.x - 250f, sheepArm.localPosition.y);
				swipeCount++;
				doMove = false;
			}
		}
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !doMove)
		{
			doMove = true;
		}
		
		if (MiniGameManager.ManagerReference.timer > 0) isComplete = (swipeCount >= 3);

		if (isComplete || MiniGameManager.ManagerReference.timer == 0f) EndGame();
	}

	void EndGame()
	{
		Invoke("DelayResetTransform", 1.5f);
		MiniGameManager.ManagerReference.didWin = isComplete;
		MiniGameManager.ManagerReference.EndMiniGame();
	}

	public override void ResetGame()
	{
		swipeCount = 0;
		isComplete = false;
		doStartGame = true;
	}

	void DelayResetTransform()
	{
		sheepArm.localPosition = new Vector2(1000, 0);
	}
}