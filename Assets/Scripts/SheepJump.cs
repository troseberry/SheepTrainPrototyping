using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepJump : MonoBehaviour
{
    public static SheepJump SheepJumpReference;

    public GameObject sheep_01;
    public GameObject sheep_02;
    public GameObject sheep_03;
    public GameObject sheep_04;
    public GameObject sheep_05;

    IEnumerator sheepSpawnCoroutine;

    bool moveSheep = true;

    public bool failedEarly = false;

    void Start ()
    {
        SheepJumpReference = this;
        sheepSpawnCoroutine = SpawnSheep();
	}


    void Update ()
    {
        if (MiniGameManager.ManagerReference.IsInGame() && moveSheep)
        {
            StartCoroutine(sheepSpawnCoroutine);
            moveSheep = false;
        }

        if (failedEarly || MiniGameManager.ManagerReference.timer == 0) CloseGame();
	}


    IEnumerator SpawnSheep()
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


    void CloseGame()
    {
        StopCoroutine(sheepSpawnCoroutine);

        MiniGameManager.ManagerReference.didWin = !failedEarly;
        MiniGameManager.ManagerReference.EndMiniGame();
        gameObject.SetActive(false);
    }

    public void ResetGame()
    {
        moveSheep = true;
        failedEarly = false;

        sheep_01.SetActive(true);
        sheep_02.SetActive(false);
        sheep_03.SetActive(false);
        sheep_04.SetActive(false);
        sheep_05.SetActive(false);
    }

}
