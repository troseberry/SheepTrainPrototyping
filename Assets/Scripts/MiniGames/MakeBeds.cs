using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MakeBeds : MiniGameScript 
{
	public static MakeBeds MakeBedsReference;
	public static float timeLimit = 2.5f;
	bool isComplete;

	public Transform pinchPoint_01;
	public Transform pinchPoint_02;
	public Transform pinchPoint_03;

	Vector2 randomWrinklePos_01 = Vector2.zero;
	Vector2 randomWrinklePos_02 = Vector2.zero;
	Vector2 randomWrinklePos_03 = Vector2.zero;	

	bool doStartGame;

	void Start () 
	{
		MakeBedsReference = this;
		doStartGame = true;
	}
	
	void Update () 
	{
		if (!isComplete && PlayerMiniGameHandler.IsInGame() && doStartGame)
		{
			RandomizeWrinklePositions();
			doStartGame = false;
		}

		if (PlayerMiniGameHandler.timer > 0) 
		{
			isComplete = (pinchPoint_01.GetComponent<PinchPoint>().IsPinchPointDone() && pinchPoint_02.GetComponent<PinchPoint>().IsPinchPointDone() && pinchPoint_03.GetComponent<PinchPoint>().IsPinchPointDone());
		}

		if (isComplete || PlayerMiniGameHandler.timer == 0f) EndGame();

	}

	void RandomizeWrinklePositions()
	{
		randomWrinklePos_01 = new Vector2(Random.Range(-700, -450), Random.Range(-300, 300));
		randomWrinklePos_02 = new Vector2(Random.Range(-250, 250), Random.Range(-300, 250));
		randomWrinklePos_03 = new Vector2(Random.Range(450, 700), Random.Range(-300, 300));

	
		pinchPoint_01.localPosition = randomWrinklePos_01;
		pinchPoint_02.localPosition = randomWrinklePos_02;
		pinchPoint_03.localPosition = randomWrinklePos_03;
	}

	void EndGame()
	{
		PlayerMiniGameHandler.EndMiniGame(isComplete);
	}

	public override void ResetGame()
	{
		pinchPoint_01.GetComponent<PinchPoint>().ResetPinchPoint();
		pinchPoint_02.GetComponent<PinchPoint>().ResetPinchPoint();
		pinchPoint_03.GetComponent<PinchPoint>().ResetPinchPoint();

		RandomizeWrinklePositions();

		pinchPoint_01.GetComponent<PinchPoint>().EnableImage();
		pinchPoint_02.GetComponent<PinchPoint>().EnableImage();
		pinchPoint_03.GetComponent<PinchPoint>().EnableImage();

		isComplete = false;
		doStartGame = true;
	}

	public override float GetTimeLimit() { return timeLimit; }
}
