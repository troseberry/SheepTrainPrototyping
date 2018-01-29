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

	public Transform teaGroup;
	
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
		TeapotBehavior.TeapotReference.ResetTeapotBlocker();
		TeapotBehavior.TeapotReference.ResetTeapotPosition();

		teacupOne.ResetTeacup();
		teacupTwo.ResetTeacup();
		teacupThree.ResetTeacup();

		// Need to reset individual tea objs
		for (int i = 1; i < teaGroup.childCount; i++)
		{
			teaGroup.GetChild(i).GetComponent<TeaBehavior>().ResetStartPosition();
		}
	}

	public override float GetTimeLimit() { return timeLimit; }
}