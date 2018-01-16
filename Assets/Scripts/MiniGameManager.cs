using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniGameManager : MonoBehaviour 
{
    public static MiniGameManager ManagerReference;

    public enum GameName {NONE, 
                        SPEED_LEVERS, PRESSURE_VALVE, FLICK_FUEL,
                        SOUP_FLY, CLEAR_TABLE, SERVE_TEA,
                        WOOL_CUTS, MUSTACHE_ROLL, SWEEP_WOOL,
                        SHEEP_JUMP, WAKE_GUESTS, MAKE_BEDS,
                        CHECK_INVENTORY, CHECK_TICKETS, SAVE_SHEEP};

    public GameName currentGame;
    private int currentGameIndex;

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

    //Boiler
    public GameObject speedLeversGame, pressureValveGame, flickFuelGame;

    //Dining
    public GameObject soupFlyGame, clearTableGame, serveTeaGame;

    //Salon
    public GameObject woolCutsGame, mustacheRollGame, sweepWoolGame;

    //Sleeper
    public GameObject sheepJumpGame, wakeGuestsGame, makeBedsGame;

    //Caboose
    public GameObject checkInventoryGame, checkTicketsGame, saveSheepGame;

    private GameObject[] minigamesArray;

	void Start () 
	{
        ManagerReference = this;

        currentGame = GameName.NONE;

        inGame = false;
        didWin = false;
        stopTimer = false;

        minigamesArray = new GameObject[]
        {
            speedLeversGame, pressureValveGame, flickFuelGame,
            soupFlyGame, clearTableGame, serveTeaGame,
            woolCutsGame, mustacheRollGame, sweepWoolGame,
            sheepJumpGame, wakeGuestsGame, makeBedsGame,
            checkInventoryGame, checkTicketsGame, saveSheepGame
        };
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

        minigamesArray[currentGameIndex].SetActive(false);
        minigamesArray[currentGameIndex].GetComponent<MiniGameScript>().ResetGame();

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
            currentGameIndex = 0;
            speedLeversGame.SetActive(true);
            inGame = true;
        }
    }

    public void OpenPressureValve()
    {
        timer = PressureValve.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.PRESSURE_VALVE;
        currentGameIndex = 1;
        pressureValveGame.SetActive(true);
        inGame = true;
    }

    public void OpenFlickFuel()
    {
        timer = FlickFuel.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.FLICK_FUEL;
        currentGameIndex = 2;
        flickFuelGame.SetActive(true);
        inGame = true;
    }



    public void OpenSoupFly()
    {
        timer = SoupFly.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.SOUP_FLY;
        currentGameIndex = 3;
        soupFlyGame.SetActive(true);
        inGame = true;
    }

    public void OpenClearTable()
    {
        currentGameIndex = 4;

    }

    public void OpenServeTea()
    {
        currentGameIndex = 5;
    }



    public void OpenWoolCuts()
    {
        timer = WoolCuts.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.WOOL_CUTS;
        currentGameIndex = 6;
        woolCutsGame.SetActive(true);
        inGame = true;
    }

    public void OpenMustahceRoll()
    {
        timer = MustacheRoll.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.MUSTACHE_ROLL;
        currentGameIndex = 7;
        mustacheRollGame.SetActive(true);
        inGame = true;
    }

    public void OpenSweepWool()
    {
        timer = SweepWool.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.SWEEP_WOOL;
        currentGameIndex = 8;
        sweepWoolGame.SetActive(true);
        inGame = true;
    }



    public void OpenSheepJump()
    {
        timer = SheepJump.timeLimit;
        StartGeneralTasks();
    
        currentGame = GameName.SHEEP_JUMP;
        currentGameIndex = 9;
        sheepJumpGame.SetActive(true);
        inGame = true;
    }

    public void OpenWakeGuests()
    {
        timer = WakeGuests.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.WAKE_GUESTS;
        currentGameIndex = 10;
        wakeGuestsGame.SetActive(true);
        inGame = true;
    }

    public void OpenMakeBeds()
    {
        timer = MakeBeds.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.MAKE_BEDS;
        currentGameIndex = 11;
        makeBedsGame.SetActive(true);
        inGame = true;
    }



    public void OpenCheckInventory()
    {
        timer = CheckInventory.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.CHECK_INVENTORY;
        currentGameIndex = 12;
        checkInventoryGame.SetActive(true);
        inGame = true;
    }

    public void OpenCheckTickets()
    {
        timer = CheckTickets.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.CHECK_TICKETS;
        currentGameIndex = 13;
        checkTicketsGame.SetActive(true);
        inGame = true;
    }

    public void OpenSaveSheep()
    {
        timer = SaveSheep.timeLimit;
        StartGeneralTasks();

        currentGame = GameName.SAVE_SHEEP;
        currentGameIndex = 14;
        saveSheepGame.SetActive(true);
        inGame = true;
    }

    

    

    public bool IsInGame()
    {
        return inGame;
    }

}
