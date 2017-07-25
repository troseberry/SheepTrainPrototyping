using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MakeBeds : MonoBehaviour 
{
	public static MakeBeds MakeBedsReference;
	public static float timeLimit = 2.5f;
	bool isComplete;

	public Transform wrinkle_01;
	public Transform wrinkle_02;
	public Transform wrinkle_03;

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
		if (!isComplete && MiniGameManager.ManagerReference.IsInGame() && doStartGame)
		{
			randomizeWrinklePositions();
			doStartGame = false;
		}

		if (MiniGameManager.ManagerReference.timer > 0) 
		{
			isComplete = (wrinkle_01.GetComponent<Wrinkle>().isWrinkleDone() && wrinkle_02.GetComponent<Wrinkle>().isWrinkleDone() && wrinkle_03.GetComponent<Wrinkle>().isWrinkleDone());
		}

		if (isComplete || MiniGameManager.ManagerReference.timer == 0f) EndGame();

	}

	void randomizeWrinklePositions()
	{
		randomWrinklePos_01 = new Vector2(Random.Range(-700, -450), Random.Range(-300, 300));
		randomWrinklePos_02 = new Vector2(Random.Range(-250, 250), Random.Range(-300, 250));
		randomWrinklePos_03 = new Vector2(Random.Range(450, 700), Random.Range(-300, 300));

	
		wrinkle_01.localPosition = randomWrinklePos_01;
		wrinkle_02.localPosition = randomWrinklePos_02;
		wrinkle_03.localPosition = randomWrinklePos_03;
	}

	void EndGame()
	{
		MiniGameManager.ManagerReference.didWin = isComplete;
        MiniGameManager.ManagerReference.EndMiniGame();
	}

	public void ResetGame()
	{
		wrinkle_01.GetComponent<Wrinkle>().ResetWrinkle();
		wrinkle_02.GetComponent<Wrinkle>().ResetWrinkle();
		wrinkle_03.GetComponent<Wrinkle>().ResetWrinkle();

		randomizeWrinklePositions();

		wrinkle_01.GetComponent<Wrinkle>().EnableImage();
		wrinkle_02.GetComponent<Wrinkle>().EnableImage();
		wrinkle_03.GetComponent<Wrinkle>().EnableImage();

		doStartGame = true;
	}
}
