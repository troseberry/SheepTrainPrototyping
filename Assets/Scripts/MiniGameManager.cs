using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MiniGameManager : MonoBehaviour 
{
    public static MiniGameManager ManagerReference;

    public enum GameName {SPEED_LEVERS, PRESSURE_VALVE, FLICK_FUEL,
                        SOUP_FLY, CLEAR_TABLE, SERVE_TEA,
                        WOOL_CUTS, MUSTACHE_ROLL, SWEEP_WOOL,
                        SHEEP_JUMP, WAKE_GUESTS, MAKE_BEDS,
                        CHECK_INVENTORY, CHECK_TICKETS, SAVE_SHEEP,
                        NONE};

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
    private MiniGameScript[] minigameScripts;

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

        minigameScripts = new MiniGameScript[]
        {
            speedLeversGame.GetComponent<MiniGameScript>(), pressureValveGame.GetComponent<MiniGameScript>(), flickFuelGame.GetComponent<MiniGameScript>(),
            soupFlyGame.GetComponent<MiniGameScript>(), clearTableGame.GetComponent<MiniGameScript>(), serveTeaGame.GetComponent<MiniGameScript>(),
            woolCutsGame.GetComponent<MiniGameScript>(), mustacheRollGame.GetComponent<MiniGameScript>(), sweepWoolGame.GetComponent<MiniGameScript>(),
            sheepJumpGame.GetComponent<MiniGameScript>(), wakeGuestsGame.GetComponent<MiniGameScript>(), makeBedsGame.GetComponent<MiniGameScript>(),
            checkInventoryGame.GetComponent<MiniGameScript>(), checkTicketsGame.GetComponent<MiniGameScript>(), saveSheepGame.GetComponent<MiniGameScript>()
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

    public void OpenMiniGame()
    {
        if (!inGame)
        {
            int gameIndex = int.Parse(EventSystem.current.currentSelectedGameObject.name.Substring(0, 2));

            timer = minigameScripts[gameIndex].GetTimeLimit();
            StartGeneralTasks();

            currentGame = (GameName) gameIndex;
            currentGameIndex = gameIndex;
            minigamesArray[gameIndex].SetActive(true);

            inGame = true;
        }
    }

    public bool IsInGame()
    {
        return inGame;
    }

}
