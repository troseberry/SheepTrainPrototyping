//Null reference exception when trying to reset

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustacheRoll : MiniGameScript 
{
	public static MustacheRoll MustacheRollReference;
	public static float timeLimit = 5f;

	private bool isComplete = false;
	private bool doStartGame = true;
	private bool didEndGame = false;

	public PinchPoint pinchPointLeft;
	public PinchPoint pinchPointCenter;
	public PinchPoint pinchPointRight;

	void Start () 
	{
		MustacheRollReference = this;
	}
	
	void Update ()
	{
		if (!isComplete && PlayerMiniGameHandler.IsInGame() && doStartGame)
		{
			doStartGame = false;
		}

		if (PlayerMiniGameHandler.timer > 0)
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

		if ((isComplete || PlayerMiniGameHandler.timer == 0f) && !didEndGame)
		{
			EndGame();
			didEndGame = true;
		}
	}

	void EndGame()
	{
		PlayerMiniGameHandler.EndMiniGame(isComplete);
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
		didEndGame = false;
	}

	public override float GetTimeLimit() { return timeLimit; }
}