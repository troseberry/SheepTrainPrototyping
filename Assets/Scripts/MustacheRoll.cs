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
		if (!isComplete && MiniGameManager.ManagerReference.IsInGame() && doStartGame)
		{
			doStartGame = false;
		}

		if (MiniGameManager.ManagerReference.timer > 0)
		{
			isComplete = (pinchPointCenter.IsPinchPointDone() && pinchPointRight.IsPinchPointDone() && pinchPointLeft.IsPinchPointDone());
			
			if (!isComplete)
			{
				if (pinchPointCenter.IsPinchPointDone() &&!pinchPointLeft.IsPinchPointDone())
				{
					pinchPointLeft.transform.gameObject.SetActive(true);
					pinchPointRight.transform.gameObject.SetActive(true);
				}
			}
		}

		if (isComplete || MiniGameManager.ManagerReference.timer == 0f) EndGame();
	}

	void EndGame()
	{
		MiniGameManager.ManagerReference.didWin = isComplete;
		MiniGameManager.ManagerReference.EndMiniGame();
	}

	public override void ResetGame()
	{
		pinchPointCenter.ResetPinchPoint();
		pinchPointLeft.ResetPinchPoint();
		pinchPointRight.ResetPinchPoint();

		pinchPointCenter.transform.gameObject.SetActive(true);
		pinchPointLeft.transform.gameObject.SetActive(false);
		pinchPointRight.transform.gameObject.SetActive(false);

		isComplete = false;
		doStartGame = true;
	}
}