/*
** Encounters issues when the mini game is opened immediately after closing. Up/Down texts not being correctly reset
** Immediately being you spam the mini game button as soon as the mini game itself closes
** In a real game situation, logic would not allow for the mini game to be reinitiated so soon after being completed.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpeedLevers : MonoBehaviour 
{
    public static SpeedLevers SpeedLeversReference;
    public static float timeLimit = 3.0f;

	int matchPos_01 = 0;
	int matchPos_02 = 0;
	int matchPos_03 = 0;

	Transform matchText_01;
	Transform matchText_02;
	Transform matchText_03;

	Slider lever_01;
	Slider lever_02;
	Slider lever_03;

	bool isComplete = false;

	bool leverOneDone;
	bool leverTwoDone;
	bool leverThreeDone;

    bool doStartGame = true;

	void Start () 
	{
        SpeedLeversReference = this;

        matchText_01 = transform.Find("MatchPositions/Position_01");
		matchText_02 = transform.Find("MatchPositions/Position_02");
		matchText_03 = transform.Find("MatchPositions/Position_03");

		lever_01 = transform.Find("Lever_01").GetComponent<Slider>();
		lever_02 = transform.Find("Lever_02").GetComponent<Slider>();
		lever_03 = transform.Find("Lever_03").GetComponent<Slider>();
	}
	
	void Update () 
	{

        if (!isComplete && MiniGameManager.ManagerReference.IsInGame() && doStartGame)
        {
            RandomizeMatchLeverPosition();
            doStartGame = false;
        }

		leverOneDone = (lever_01.value == matchPos_01);
		leverTwoDone = (lever_02.value == matchPos_02);
		leverThreeDone = (lever_03.value == matchPos_03);

		if (MiniGameManager.ManagerReference.timer > 0) isComplete = (leverOneDone && leverTwoDone && leverThreeDone);

        if (isComplete || MiniGameManager.ManagerReference.timer == 0f) EndGame();


        // DebugPanel.Log("Match 01: ", matchPos_01);
		// DebugPanel.Log("Match 02: ", matchPos_02);
		// DebugPanel.Log("Match 03: ", matchPos_03);

		// DebugPanel.Log("Lever 01 Done: ", leverOneDone);
		// DebugPanel.Log("Lever 02 Done: ", leverTwoDone);
		// DebugPanel.Log("Lever 03 Done: ", leverThreeDone);
	}

    void RandomizeMatchLeverPosition()
    {
        matchPos_01 = (int)Mathf.Round(Random.Range(0f, 1f));
        matchPos_02 = (int)Mathf.Round(Random.Range(0f, 1f));
        matchPos_03 = (int)Mathf.Round(Random.Range(0f, 1f));

        if (matchPos_01 == 0 && matchPos_02 == 0 && matchPos_03 == 0)
        {
            int changeRandomPos = (int)Mathf.Round(Random.Range(0f, 2f));
            switch (changeRandomPos)
            {
                case 0:
                    matchPos_01 = 1;
                    return;
                case 1:
                    matchPos_02 = 1;
                    return;
                case 2:
                    matchPos_03 = 1;
                    return;
            }
        }

        matchText_01.GetChild(matchPos_01).GetComponent<Text>().enabled = true;
		matchText_02.GetChild(matchPos_02).GetComponent<Text>().enabled = true;
		matchText_03.GetChild(matchPos_03).GetComponent<Text>().enabled = true;
    }

    void EndGame()
    {
        MiniGameManager.ManagerReference.didWin = isComplete;
        MiniGameManager.ManagerReference.EndMiniGame();
    }


    public void ResetGame ()
    {
        matchText_01.GetChild(0).GetComponent<Text>().enabled = false;
        matchText_01.GetChild(1).GetComponent<Text>().enabled = false;
        matchText_02.GetChild(0).GetComponent<Text>().enabled = false;
        matchText_02.GetChild(1).GetComponent<Text>().enabled = false;
        matchText_03.GetChild(0).GetComponent<Text>().enabled = false;
        matchText_03.GetChild(1).GetComponent<Text>().enabled = false;

        isComplete = false;

        lever_01.value = 0;
        lever_02.value = 0;
        lever_03.value = 0;

        leverOneDone = false;
        leverTwoDone = false;
        leverThreeDone = false;

        doStartGame = true;
    }
}
