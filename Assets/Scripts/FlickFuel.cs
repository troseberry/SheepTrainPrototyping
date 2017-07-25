﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickFuel : MonoBehaviour
{
    public static FlickFuel FlickFuelReference;
    public static float timeLimit = 5.0f;

    int coalCaughtAmount = 0;

    public CoalBehavior coal_01;
    public CoalBehavior coal_02;
    public CoalBehavior coal_03;
    public CoalBehavior coal_04;
    public CoalBehavior coal_05;

    bool isComplete;
    public static bool doStartGame = true;

    void Start ()
    {
        FlickFuelReference = this;
	}


    void Update ()
    {
        isComplete = (coalCaughtAmount >= 3);

        if (isComplete || MiniGameManager.ManagerReference.timer == 0f) Invoke("CloseGame", 0.25f);

        DebugPanel.Log("Coal Count: ", coalCaughtAmount);
    }

    public void CaughtCoal()
    {
        coalCaughtAmount++;
    }

    void CloseGame()
    {
        FurnaceBehavior.FurnaceBehaviorReference.StopFurnaceMovement();

        MiniGameManager.ManagerReference.didWin = isComplete;
        MiniGameManager.ManagerReference.EndMiniGame();
        gameObject.SetActive(false);
    }

    public void ResetGame()
    {
        doStartGame = true;

        coalCaughtAmount = 0;

        coal_01.ResetPosition();
        coal_01.ShowCoal();

        coal_02.ResetPosition();
        coal_02.ShowCoal();

        coal_03.ResetPosition();
        coal_03.ShowCoal();

        coal_04.ResetPosition();
        coal_04.ShowCoal();

        coal_05.ResetPosition();
        coal_05.ShowCoal();
    }
}
