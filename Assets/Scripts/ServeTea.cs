using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeTea : MiniGameScript 
{
	public static ServeTea ServeTeaReference;
	public static float timeLimit = 7.5f;
	private bool isComplete;
	private bool doStartGame;

	public TeacupBehavior teacupOne;
	public TeacupBehavior teacupTwo;
	public TeacupBehavior teacupThree;

	
	void Start () 
	{
		ServeTeaReference = this;
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
			isComplete = (teacupOne.IsCupFull() && teacupTwo.IsCupFull() && teacupThree.IsCupFull());
		}

		if (isComplete || MiniGameManager.timer == 0f) EndGame();
	}

	void EndGame()
	{
		MiniGameManager.EndMiniGame(isComplete);
	}

	public override void ResetGame()
	{
		TeapotBehavior.TeapotReference.ResetTeapotPosition();

		teacupOne.ResetTeaCount();
		teacupTwo.ResetTeaCount();
		teacupThree.ResetTeaCount();

		// Need to reset individual tea objs
	}

	public override float GetTimeLimit() { return timeLimit; }
}