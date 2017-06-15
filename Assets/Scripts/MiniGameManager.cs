using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour 
{
    public static MiniGameManager ManagerReference;

    bool inGame;

	public float timer;
	public Text taskTimerText;
	String formattedTimer;

    public bool didWin;

    public GameObject generalElements;
    public GameObject passText;
    public GameObject failText;

    public GameObject speedLevers;

	void Start () 
	{
        ManagerReference = this;
        inGame = false;
        didWin = false;

        //taskTimerText = transform.Find("TaskTimer").GetComponent<Text>();
        timer = 5f;
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
        timer = 5.0f;
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

        SpeedLevers.SpeedLeversReference.ResetGame();
    }

    public void OpenSpeedMiniGame()
    {
        StartGeneralTasks();
        
        speedLevers.SetActive(true);
        inGame = true;
    }

    
}
