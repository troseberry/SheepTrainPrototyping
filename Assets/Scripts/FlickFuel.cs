﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickFuel : MiniGameScript
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
        //this game is missing the logic that helps start the game (setting doStartGame true if manager is in game and this game isn't complete) even after the mini game object has been initialied. But it appears to correctly start every time?

        if (PlayerMiniGameHandler.timer > 0) isComplete = (coalCaughtAmount >= 3);

        if (isComplete || PlayerMiniGameHandler.timer == 0f) EndGame();
    }

    public void CaughtCoal()
    {
        coalCaughtAmount++;
    }

    void EndGame()
    {
        FurnaceBehavior.FurnaceBehaviorReference.StopFurnaceMovement();

        PlayerMiniGameHandler.EndMiniGame(isComplete);
    }

    public override void ResetGame()
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

    public override float GetTimeLimit() { return timeLimit; }
}
