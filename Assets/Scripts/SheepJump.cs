using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepJump : MonoBehaviour
{
    public static SheepJump SheepJumpReference;
    public static float timeLimit = 7.5f;

    public GameObject sheep_01;
    public GameObject sheep_02;
    public GameObject sheep_03;
    public GameObject sheep_04;
    public GameObject sheep_05;

    bool moveSheep = true;

    public bool failedEarly = false;

    void Start ()
    {
        SheepJumpReference = this;
	}


    void Update ()
    {
        if (MiniGameManager.ManagerReference.IsInGame() && moveSheep)
        {
            StartCoroutine(StartSpawnSheep());
            moveSheep = false;
        }

        if (failedEarly || MiniGameManager.ManagerReference.timer == 0) CloseGame();
	}


    //Doing coroutine by method name allows you to call StopCoroutine.
    //For some reason, storing coroutine as an IEnumerator reference doens't allow use of StopCoroutine
    IEnumerator StartSpawnSheep()
    {
        StartCoroutine("SpawnSheep");
        yield return null;
        if (failedEarly || MiniGameManager.ManagerReference.timer == 0) StopCoroutine("SpawnSheep");
        
    }


    IEnumerator SpawnSheep()
    {
        while (MiniGameManager.ManagerReference.IsInGame())
        {
            //sheep_01 is already active at start
            yield return new WaitForSeconds(1.25f);

            sheep_02.SetActive(true);
            yield return new WaitForSeconds(1.25f);

            sheep_03.SetActive(true);
            yield return new WaitForSeconds(1.25f);

            sheep_04.SetActive(true);
            yield return new WaitForSeconds(1.25f);

            sheep_05.SetActive(true);
        }
    }


    void CloseGame()
    {
        MiniGameManager.ManagerReference.didWin = !failedEarly;
        MiniGameManager.ManagerReference.EndMiniGame();
        gameObject.SetActive(false);
    }

    public void ResetGame()
    {
        moveSheep = true;
        failedEarly = false;

        sheep_01.SetActive(true);
        sheep_01.GetComponent<JumpingSheepBehavior>().ResetPosition();
        sheep_02.SetActive(false);
        sheep_02.GetComponent<JumpingSheepBehavior>().ResetPosition();
        sheep_03.SetActive(false);
        sheep_03.GetComponent<JumpingSheepBehavior>().ResetPosition();
        sheep_04.SetActive(false);
        sheep_04.GetComponent<JumpingSheepBehavior>().ResetPosition();
        sheep_05.SetActive(false);
        sheep_05.GetComponent<JumpingSheepBehavior>().ResetPosition();
    }

}
