using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheepJump : MiniGameScript
{
    public static SheepJump SheepJumpReference;
    public static float timeLimit = 7.5f;

    public GameObject sheep_01;
    public GameObject sheep_02;
    public GameObject sheep_03;
    public GameObject sheep_04;
    public GameObject sheep_05;

    public SpriteRenderer passenger;
    public SpriteRenderer ground;
    public RawImage fakePassenger;
    public RawImage fakeGround;

    bool doStartGame = true;
    public bool failedEarly = false;

    void Start ()
    {
        SheepJumpReference = this;
	}


    void Update ()
    {
        if (MiniGameManager.ManagerReference.IsInGame() && doStartGame)
        {
            StartCoroutine(StartSpawnSheep());
            doStartGame = false;
        }

        if (failedEarly || MiniGameManager.ManagerReference.timer == 0) EndGame();
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


    void EndGame()
    {
        passenger.enabled = false;
        ground.enabled = false;

        fakePassenger.enabled = true;
        fakeGround.enabled = true;

        MiniGameManager.ManagerReference.didWin = !failedEarly;
        MiniGameManager.ManagerReference.EndMiniGame();
    }

    public override void ResetGame()
    {
        doStartGame = true;
        failedEarly = false;

        passenger.enabled = true;
        ground.enabled = true;

        fakePassenger.enabled = false;
        fakeGround.enabled = false;

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

    public override float GetTimeLimit() { return timeLimit; }
}
