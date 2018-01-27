using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
// using UnityEngine.Networking;


public class MiniGameManager : MonoBehaviour 
{
    // [SyncVar (hook = "TestSyncText")]
    // public string syncTestString = "None";

    // private Text syncTestText;
    // private Transform playerTarget;

    public static MiniGameManager ManagerReference;

    public enum GameName {SPEED_LEVERS, PRESSURE_VALVE, FLICK_FUEL,
                        SOUP_FLY, CLEAR_TABLE, SERVE_TEA,
                        WOOL_CUTS, MUSTACHE_ROLL, SWEEP_WOOL,
                        SHEEP_JUMP, WAKE_GUESTS, MAKE_BEDS,
                        TAKE_INVENTORY, CHECK_TICKETS, SAVE_SHEEP,
                        NONE};

    public GameName currentGame;
    private int currentGameIndex;

    private static bool inGame;

	public static float timer;
    
	public Text taskTimerText;
	String formattedTimer;
    private static bool stopTimer;

    public static bool didWin;

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
    public GameObject takeInventoryGame, checkTicketsGame, saveSheepGame;

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
            takeInventoryGame, checkTicketsGame, saveSheepGame
        };

        minigameScripts = new MiniGameScript[]
        {
            speedLeversGame.GetComponent<MiniGameScript>(), pressureValveGame.GetComponent<MiniGameScript>(), flickFuelGame.GetComponent<MiniGameScript>(),
            soupFlyGame.GetComponent<MiniGameScript>(), clearTableGame.GetComponent<MiniGameScript>(), serveTeaGame.GetComponent<MiniGameScript>(),
            woolCutsGame.GetComponent<MiniGameScript>(), mustacheRollGame.GetComponent<MiniGameScript>(), sweepWoolGame.GetComponent<MiniGameScript>(),
            sheepJumpGame.GetComponent<MiniGameScript>(), wakeGuestsGame.GetComponent<MiniGameScript>(), makeBedsGame.GetComponent<MiniGameScript>(),
            takeInventoryGame.GetComponent<MiniGameScript>(), checkTicketsGame.GetComponent<MiniGameScript>(), saveSheepGame.GetComponent<MiniGameScript>()
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

    // public void SetPlayerTarget(Transform player)
    // {
    //     playerTarget = player;
    //     syncTestText = playerTarget.GetComponentInChildren<Text>();
    // }

    void StartGeneralTasks()
    {
        generalElements.SetActive(true);
    }

    public static void EndMiniGame(bool endStatus)
    {
        didWin = endStatus;

        stopTimer = true;
        ManagerReference.endGameOverlay.SetActive(true);
        if (didWin)
        {
            ManagerReference.passText.SetActive(true);
        }
        else
        {
            ManagerReference.failText.SetActive(true);
        }
        ManagerReference.Invoke("CloseMiniGame", 1.5f);
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

            // syncTestString = currentGame.ToString();
            // TestSyncText(syncTestString);
        }
    }

    public static bool IsInGame()
    {
        return inGame;
    }


    // void TestSyncText(string testString)
    // {
    //     if (!isServer)
    //     {
    //         return;
    //     }

    //     syncTestText.text = testString;
    // }

}
