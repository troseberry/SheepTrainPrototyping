using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeTea : MiniGameScript 
{
	public static ServeTea ServeTeaReference;
	public static float timeLimit = 7.5f;

	private bool isComplete = false;
	private bool doStartGame = true;
	private bool didEndGame = false;

	public TeacupBehavior teacupOne;
	public TeacupBehavior teacupTwo;
	public TeacupBehavior teacupThree;

	public Transform teaGroup;
	
	void Start () 
	{
		ServeTeaReference = this;
	}
	
	void Update () 
	{
		if (!isComplete && PlayerMiniGameHandler.IsInGame() && doStartGame)
		{
			doStartGame = false;
		}

		if (PlayerMiniGameHandler.timer > 0)
		{
			isComplete = (teacupOne.IsCupFull() && teacupTwo.IsCupFull() && teacupThree.IsCupFull());
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

		isComplete = false;
		doStartGame = true;
		didEndGame = false;
	}

	public override float GetTimeLimit() { return timeLimit; }
}