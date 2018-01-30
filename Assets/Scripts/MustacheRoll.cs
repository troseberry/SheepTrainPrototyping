//Null reference exception when trying to reset

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustacheRoll : MiniGameScript 
{
	public static MustacheRoll MustacheRollReference;
	public static float timeLimit = 5f;
	private bool isComplete;

	public PinchPoint pinchPointLeft;
	public PinchPoint pinchPointCenter;
	public PinchPoint pinchPointRight;

	private bool doStartGame;

	void Start () 
	{
		MustacheRollReference = this;
		doStartGame = true;
	}
	
	void Update ()
	{
		if (!isComplete && MiniGameManager.IsInGame() && doStartGame)
		{
			doStartGame = false;
		}

		if (MiniGameManager.timer > 0)
		{
			isComplete = (pinchPointCenter.IsPinchPointDone() && pinchPointRight.IsPinchPointDone() && pinchPointLeft.IsPinchPointDone());
			
			if (!isComplete)
			{
				if (pinchPointCenter.IsPinchPointDone())
				{
					if (!pinchPointLeft.IsPinchPointDone())pinchPointLeft.EnableImage();
					if (!pinchPointRight.IsPinchPointDone()) pinchPointRight.EnableImage();
				}
			}
		}

		if (isComplete || MiniGameManager.timer == 0f) EndGame();
	}

	void EndGame()
	{
		MiniGameManager.EndMiniGame(isComplete);
	}

	public override void ResetGame()
	{
		pinchPointCenter.ResetPinchPoint();
		pinchPointLeft.ResetPinchPoint();
		pinchPointRight.ResetPinchPoint();

		pinchPointCenter.EnableImage();
		pinchPointLeft.DisableImage();
		pinchPointRight.DisableImage();

		isComplete = false;
		doStartGame = true;
	}

	public override float GetTimeLimit() { return timeLimit; }
}