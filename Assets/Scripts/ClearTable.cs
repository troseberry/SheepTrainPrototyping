using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTable : MiniGameScript 
{
	public static ClearTable ClearTableReference;
	public static float timeLimit = 3.5f;
	private bool isComplete;
	private bool doStartGame;

	public Rigidbody2D tableObjOne, tableObjTwo, tableObjThree;
	private Vector2 startPosOne, startPosTwo, startPosThree;
	private bool boostOne, boostTwo, boostThree;

	private Vector3 objForce;
	private float shakeDuration = 0f;



	void Start () 
	{
		ClearTableReference = this;
		doStartGame = true;

		boostOne =  false;
		boostTwo = false;
		boostThree = false;

		startPosOne = tableObjOne.transform.position;
		startPosTwo = tableObjTwo.transform.position;
		startPosThree = tableObjThree.transform.position;
	}
	
	void Update () 
	{
		if (!isComplete && MiniGameManager.IsInGame() && doStartGame)
		{
			doStartGame = false;
		}

		if (MiniGameManager.timer > 0) isComplete = (boostOne && boostTwo && boostThree);

		if (isComplete || MiniGameManager.timer == 0f) EndGame();



		// Debug.Log("Force Sq: " + objForce.sqrMagnitude);
		// Debug.Log("Shake Duration: " + shakeDuration);
		objForce = Input.acceleration;

		if (objForce.sqrMagnitude >= 3f)
		{
			shakeDuration += Time.deltaTime;
		}


		if (shakeDuration >= 0.5f)
		{
			tableObjTwo.isKinematic = false;
			if (!boostTwo) 
			{
				tableObjTwo.AddForce(2.5f * Vector2.up, ForceMode2D.Impulse);
				tableObjTwo.AddTorque(1f, ForceMode2D.Impulse);
				boostTwo = true;
			}
		}

		if (shakeDuration >= 1f)
		{
			tableObjOne.isKinematic = false;
			if (!boostOne) 
			{
				tableObjOne.AddForce(2.5f * Vector2.up, ForceMode2D.Impulse);
				tableObjOne.AddTorque(-1f, ForceMode2D.Impulse);
				boostOne = true;
			}
		}

		if (shakeDuration >= 1.5f)
		{
			tableObjThree.isKinematic = false;
			if (!boostThree) 
			{
				tableObjThree.AddForce(2.5f * Vector2.up, ForceMode2D.Impulse);
				tableObjThree.AddTorque(-1f, ForceMode2D.Impulse);
				boostThree = true;
			}
		}
	}

	void EndGame()
	{
		MiniGameManager.EndMiniGame(isComplete);
	}

	public override void ResetGame()
	{
		tableObjOne.isKinematic = true;
		tableObjTwo.isKinematic = true;
		tableObjThree.isKinematic = true;

		tableObjOne.transform.position = startPosOne;
		tableObjOne.transform.rotation = Quaternion.identity;

		tableObjTwo.transform.position = startPosTwo;
		tableObjTwo.transform.rotation = Quaternion.identity;

		tableObjThree.transform.position = startPosThree;
		tableObjThree.transform.rotation = Quaternion.identity;

		boostOne = false;
		boostTwo = false;
		boostThree = false;

		shakeDuration = 0;
	}

	public override float GetTimeLimit() { return timeLimit; }
}