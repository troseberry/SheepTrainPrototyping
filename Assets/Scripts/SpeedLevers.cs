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

	void Start () 
	{
        SpeedLeversReference = this;

        //pick 3 random lever positions
        matchPos_01 = (int)Mathf.Round(Random.Range(0f, 1f));
        matchPos_02 = (int)Mathf.Round(Random.Range(0f, 1f));
        matchPos_03 = (int)Mathf.Round(Random.Range(0f, 1f));

        matchText_01 = transform.Find("MatchPositions/Position_01");
		matchText_01.GetChild(matchPos_01).GetComponent<Text>().enabled = true;

		matchText_02 = transform.Find("MatchPositions/Position_02");
		matchText_02.GetChild(matchPos_02).GetComponent<Text>().enabled = true;

		matchText_03 = transform.Find("MatchPositions/Position_03");
		matchText_03.GetChild(matchPos_03).GetComponent<Text>().enabled = true;

		lever_01 = transform.Find("Lever_01").GetComponent<Slider>();
		lever_02 = transform.Find("Lever_02").GetComponent<Slider>();
		lever_03 = transform.Find("Lever_03").GetComponent<Slider>();
	}
	
	void Update () 
	{
		leverOneDone = (lever_01.value == matchPos_01);
		leverTwoDone = (lever_02.value == matchPos_02);
		leverThreeDone = (lever_03.value == matchPos_03);

		isComplete = (leverOneDone && leverTwoDone && leverThreeDone);

		DebugPanel.Log("Match 01: ", matchPos_01);
		DebugPanel.Log("Match 02: ", matchPos_02);
		DebugPanel.Log("Match 03: ", matchPos_03);

		DebugPanel.Log("Lever 01 Done: ", leverOneDone);
		DebugPanel.Log("Lever 02 Done: ", leverTwoDone);
		DebugPanel.Log("Lever 03 Done: ", leverThreeDone);

		DebugPanel.Log("Game Complete: ", isComplete);

        if (isComplete || MiniGameManager.ManagerReference.timer == 0f) Invoke("CloseGame", 0.25f);
	}

    void CloseGame()
    {
        matchText_01.GetChild(matchPos_01).GetComponent<Text>().enabled = false;
        matchText_02.GetChild(matchPos_02).GetComponent<Text>().enabled = false;
        matchText_03.GetChild(matchPos_03).GetComponent<Text>().enabled = false;

        MiniGameManager.ManagerReference.didWin = isComplete;
        MiniGameManager.ManagerReference.EndMiniGame();
        gameObject.SetActive(false);
        //Invoke("CloseGameOverlay", 0.5f);
    }

    //void CloseGameOverlay()
    //{
    //    gameObject.SetActive(false);
    //}

    public void ResetGame ()
    {
        isComplete = false;
        matchPos_01 = (int)Mathf.Round(Random.Range(0f, 1f));
        matchPos_02 = (int)Mathf.Round(Random.Range(0f, 1f));
        matchPos_03 = (int)Mathf.Round(Random.Range(0f, 1f));

        lever_01.value = 0;
        lever_02.value = 0;
        lever_03.value = 0;

        leverOneDone = false;
        leverTwoDone = false;
        leverThreeDone = false;

        matchText_01.GetChild(matchPos_01).GetComponent<Text>().enabled = true;
        matchText_02.GetChild(matchPos_02).GetComponent<Text>().enabled = true;
        matchText_03.GetChild(matchPos_03).GetComponent<Text>().enabled = true;

    }
}
