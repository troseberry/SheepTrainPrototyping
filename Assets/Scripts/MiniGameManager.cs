using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour 
{
    public static MiniGameManager ManagerReference;

    public enum GameName { NONE, SPEED_LEVERS, PRESSURE_VALVE, FLICK_FUEL, SHEEP_JUMP};
    public GameName currentGame;

    bool inGame;

	public float timer;
	public Text taskTimerText;
	String formattedTimer;

    public bool didWin;

    public GameObject generalElements;
    public GameObject passText;
    public GameObject failText;

    public GameObject speedLeversGame;
    public GameObject pressureValveGame;
    public GameObject flickFuelGame;

    public GameObject sheepJumpGame;

	void Start () 
	{
        ManagerReference = this;

        currentGame = GameName.NONE;

        inGame = false;
        didWin = false;

        //timer = 3f;
	}
	
	void Update () 
	{
        if (inGame)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0;
            }
            formattedTimer = string.Format("{0:00.00}", timer % 60);
            taskTimerText.text = formattedTimer;
        }

		DebugPanel.Log("Timer: ", timer);
	}

    public void OpenMiniGame ()
    {
        //uiEventSystem.currentSelectedGameObject.name;
    }

    void StartGeneralTasks()
    {
        generalElements.SetActive(true);
        //timer = 3.0f;
    }

    public void EndMiniGame()
    {
        inGame = false;
        if (didWin)
        {
            passText.SetActive(true);
        }
        else
        {
            failText.SetActive(true);
        }
        Invoke("CloseMiniGame", 1.5f);
    }

    public void CloseMiniGame()
    {
        generalElements.SetActive(false);
        passText.SetActive(false);
        failText.SetActive(false);

        if (currentGame == GameName.SPEED_LEVERS)
        {
            SpeedLevers.SpeedLeversReference.ResetGame();
        }
        else if (currentGame == GameName.PRESSURE_VALVE)
        {
            PressureValve.PressureValveReference.ResetGame();
        }
        else if (currentGame == GameName.FLICK_FUEL)
        {
            FlickFuel.FlickFuelReference.ResetGame();
        }
        else if (currentGame == GameName.SHEEP_JUMP)
        {
            SheepJump.SheepJumpReference.ResetGame();
        }
    }

    public void OpenSpeedLevers()
    {
        timer = SpeedLevers.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.SPEED_LEVERS;
        speedLeversGame.SetActive(true);
        inGame = true;
    }

    public void OpenPressureValve()
    {
        timer = PressureValve.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.PRESSURE_VALVE;
        pressureValveGame.SetActive(true);
        inGame = true;
    }

    public void OpenFlickFuel()
    {
        timer = FlickFuel.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.FLICK_FUEL;
        flickFuelGame.SetActive(true);
        inGame = true;
    }

    public void OpenSheepJump()
    {
        timer = SheepJump.timeLimit;
        StartGeneralTasks();
    
        currentGame = GameName.SHEEP_JUMP;
        sheepJumpGame.SetActive(true);
        inGame = true;
    }


    public bool IsInGame()
    {
        return inGame;
    }

}
