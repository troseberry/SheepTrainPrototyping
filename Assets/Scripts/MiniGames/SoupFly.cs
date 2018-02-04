using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupFly : MiniGameScript 
{
	public static SoupFly SoupFlyReference;
	public static float timeLimit = 2.5f;

	private bool isComplete = false;
	private bool doStartGame = true;
	private bool didEndGame = false;

	public Transform pinchPoint_01;
	public Transform pinchPoint_02;
	public Transform pinchPoint_03;

	Vector2 randomFlyPos_01 = Vector2.zero;
	Vector2 randomFlyPos_02 = Vector2.zero;
	Vector2 randomFlyPos_03 = Vector2.zero;


	void Start () 
	{
		SoupFlyReference = this;
	}
	
	void Update () 
	{
		if (!isComplete && PlayerMiniGameHandler.IsInGame() && doStartGame)
		{
			RandomizeFlyPositions();
			doStartGame = false;
		}

		if (PlayerMiniGameHandler.timer > 0)
		{
			isComplete = (pinchPoint_01.GetComponent<PinchPoint>().IsPinchPointDone() && pinchPoint_02.GetComponent<PinchPoint>().IsPinchPointDone() && pinchPoint_03.GetComponent<PinchPoint>().IsPinchPointDone());
		}

		if ((isComplete || PlayerMiniGameHandler.timer == 0f) && !didEndGame)
		{
			EndGame();
			didEndGame = true;
		}
	}

	void RandomizeFlyPositions()
	{
		randomFlyPos_01 = new Vector2(Random.Range(-300, -100), Random.Range(100, 300));
		randomFlyPos_02 = new Vector2(Random.Range(-300, 300), Random.Range(-100, -300));
		randomFlyPos_03 = new Vector2(Random.Range(150, 300), Random.Range(100, 300));
	
		pinchPoint_01.localPosition = randomFlyPos_01;
		pinchPoint_02.localPosition = randomFlyPos_02;
		pinchPoint_03.localPosition = randomFlyPos_03;
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

		RandomizeFlyPositions();

		pinchPoint_01.GetComponent<PinchPoint>().EnableImage();
		pinchPoint_02.GetComponent<PinchPoint>().EnableImage();
		pinchPoint_03.GetComponent<PinchPoint>().EnableImage();

		isComplete = false;
		doStartGame = true;
		didEndGame = false;
	}

	public override float GetTimeLimit() { return timeLimit; }
}