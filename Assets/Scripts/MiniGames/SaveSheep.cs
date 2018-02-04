using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSheep : MiniGameScript 
{
	public static SaveSheep SaveSheepReference;
	public static float timeLimit = 3f;

	private bool isComplete = false;
	private bool doStartGame = true;
	private bool didEndGame = false;
	
	public Transform sheepArm;
	private bool doMove =  true;
	private int swipeCount = 0;

	void Start () 
	{
		SaveSheepReference = this;
		doStartGame = true;
	}
	
	void Update ()
	{
		if (!isComplete && PlayerMiniGameHandler.IsInGame() && doStartGame)
		{
			// swipeCount = 0;
			// sheepArm.localPosition = new Vector2(1000, 0);
			doStartGame = false;
		}

		if (PlayerMiniGameHandler.timer > 0) isComplete = (swipeCount >= 3);

		if ((isComplete || PlayerMiniGameHandler.timer == 0f) && !didEndGame)
		{
			EndGame();
			didEndGame = true;
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
	}

	void EndGame()
	{
		Invoke("DelayResetTransform", 1.5f);
		
		PlayerMiniGameHandler.EndMiniGame(isComplete);
	}

	public override void ResetGame()
	{
		swipeCount = 0;

		isComplete = false;
		doStartGame = true;
		didEndGame = false;
	}

	void DelayResetTransform()
	{
		sheepArm.localPosition = new Vector2(1000, 0);
	}

	public override float GetTimeLimit() { return timeLimit; }
}