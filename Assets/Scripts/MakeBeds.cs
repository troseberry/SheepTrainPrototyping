using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MakeBeds : MonoBehaviour 
{
	public static MakeBeds MakeBedsReference;
	public static float timeLimit = 10.0f;
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

		isComplete = (wrinkle_01.GetComponent<Wrinkle>().isWrinkleDone() && wrinkle_02.GetComponent<Wrinkle>().isWrinkleDone() && wrinkle_03.GetComponent<Wrinkle>().isWrinkleDone());
		if (isComplete || MiniGameManager.ManagerReference.timer == 0f) CloseGame();

	}

	void randomizeWrinklePositions()
	{
		randomWrinklePos_01 = new Vector2(Random.Range(-500, -300), Random.Range(-200, 200));
		randomWrinklePos_02 = new Vector2(Random.Range(-150, 150), Random.Range(-200, 200));
		randomWrinklePos_03 = new Vector2(Random.Range(300, 500), Random.Range(-200, 200));

	
		wrinkle_01.localPosition = randomWrinklePos_01;
		wrinkle_02.localPosition = randomWrinklePos_02;
		wrinkle_03.localPosition = randomWrinklePos_03;
	}

	void CloseGame()
	{
		MiniGameManager.ManagerReference.didWin = isComplete;
        MiniGameManager.ManagerReference.EndMiniGame();
        gameObject.SetActive(false);
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
