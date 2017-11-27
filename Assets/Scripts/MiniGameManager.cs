using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniGameManager : MonoBehaviour 
{
    public static MiniGameManager ManagerReference;

    public enum GameName {NONE, SPEED_LEVERS, PRESSURE_VALVE, FLICK_FUEL, SHEEP_JUMP, WAKE_GUESTS, MAKE_BEDS,
    CHECK_INVENTORY, CHECK_TICKETS, SAVE_SHEEP, WOOL_CUTS};
    public GameName currentGame;

    bool inGame;

	public float timer;
	public Text taskTimerText;
	String formattedTimer;
    bool stopTimer;

    public bool didWin;

    public GameObject generalElements;
    public GameObject passText;
    public GameObject failText;
    public GameObject endGameOverlay;

    public GameObject speedLeversGame;
    public GameObject pressureValveGame;
    public GameObject flickFuelGame;

    public GameObject sheepJumpGame;
    public GameObject wakeGuestsGame;
    public GameObject makeBedsGame;

    public GameObject checkInventoryGame;
    public GameObject checkTicketsGame;
    public GameObject saveSheepGame;

    public GameObject woolCutsGame;


	void Start () 
	{
        ManagerReference = this;

        currentGame = GameName.NONE;

        inGame = false;
        didWin = false;
        stopTimer = false;
	}
	
	void Update () 
	{
        if (inGame && !stopTimer)
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
        DebugPanel.Log("Is In Game: ", inGame);
	}

    void StartGeneralTasks()
    {
        generalElements.SetActive(true);
    }

    public void EndMiniGame()
    {
        stopTimer = true;
        endGameOverlay.SetActive(true);
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
        endGameOverlay.SetActive(false);

        if (currentGame == GameName.SPEED_LEVERS)
        {
            speedLeversGame.SetActive(false);
            SpeedLevers.SpeedLeversReference.ResetGame();
        }
        else if (currentGame == GameName.PRESSURE_VALVE)
        {
            pressureValveGame.SetActive(false);
            PressureValve.PressureValveReference.ResetGame();
        }
        else if (currentGame == GameName.FLICK_FUEL)
        {
            flickFuelGame.SetActive(false);
            FlickFuel.FlickFuelReference.ResetGame();
        }
        else if (currentGame == GameName.SHEEP_JUMP)
        {
            sheepJumpGame.SetActive(false);
            SheepJump.SheepJumpReference.ResetGame();
        }
        else if (currentGame == GameName.WAKE_GUESTS)
        {
            wakeGuestsGame.SetActive(false);
            WakeGuests.WakeGuestsReference.ResetGame();
        }
        else if (currentGame == GameName.MAKE_BEDS)
        {
            makeBedsGame.SetActive(false);
            MakeBeds.MakeBedsReference.ResetGame();
        }
        else if (currentGame == GameName.CHECK_INVENTORY)
        {
            checkInventoryGame.SetActive(false);
            CheckInventory.CheckInventoryReference.ResetGame();
        }
        else if (currentGame == GameName.CHECK_TICKETS)
        {
            checkTicketsGame.SetActive(false);
            CheckTickets.CheckTicketsReference.ResetGame();
        }
        else if (currentGame == GameName.SAVE_SHEEP)
        {
            saveSheepGame.SetActive(false);
            SaveSheep.SaveSheepReference.ResetGame();
        }
        else if (currentGame == GameName.WOOL_CUTS)
        {
            woolCutsGame.SetActive(false);
            WoolCuts.WoolCutsReference.ResetGame();
        }
        inGame = false;
        didWin = false;
        stopTimer = false;
    }

    public void OpenSpeedLevers()
    {
        if (!inGame)
        {
            timer = SpeedLevers.timeLimit;
            StartGeneralTasks();

            currentGame = GameName.SPEED_LEVERS;
            speedLeversGame.SetActive(true);
            inGame = true;
        }
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

    public void OpenWakeGuests()
    {
        timer = WakeGuests.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.WAKE_GUESTS;
        wakeGuestsGame.SetActive(true);
        inGame = true;
    }

    public void OpenMakeBeds()
    {
        timer = MakeBeds.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.MAKE_BEDS;
        makeBedsGame.SetActive(true);
        inGame = true;
    }

    public void OpenCheckInventory()
    {
        timer = CheckInventory.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.CHECK_INVENTORY;
        checkInventoryGame.SetActive(true);
        inGame = true;
    }

    public void OpenCheckTickets()
    {
        timer = CheckTickets.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.CHECK_TICKETS;
        checkTicketsGame.SetActive(true);
        inGame = true;
    }

    public void OpenSaveSheep()
    {
        timer = SaveSheep.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.SAVE_SHEEP;
        saveSheepGame.SetActive(true);
        inGame = true;
    }

    public void OpenWoolCuts()
    {
        timer = WoolCuts.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.WOOL_CUTS;
        woolCutsGame.SetActive(true);
        inGame = true;
    }

    public bool IsInGame()
    {
        return inGame;
    }

}
