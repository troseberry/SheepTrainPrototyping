﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkedPlayerBehavior : NetworkBehaviour 
{
    public enum TransitionType { LEFT, RIGHT, UP_LEFT, UP_RIGHT, DOWN_LEFT, DOWN_RIGHT, NONE };

    bool inTransitionZone;
    public TransitionType currentTransition;

    // public GameObject genElementsPrefab;
    public Canvas readyCanvas;

	void Start () 
	{
        inTransitionZone = false;
        currentTransition = TransitionType.NONE;

        // Debug.Log(name + " Is Server: " + isServer);
	}

    public override void OnStartLocalPlayer()
    {
        TransitionScreen.TransitionScreenReference.SetPlayerTarget(transform);

        // GetComponent<SpriteRenderer>().color = Color.red;

        GetComponent<PlayerReadyHandler>().enabled = true;
        readyCanvas.enabled = true;

        GetComponent<PlayerAnimator>().enabled = true;
        for (int i = 0; i < GetComponent<Animator>().parameterCount; i++)
        {
            GetComponent<NetworkAnimator>().SetParameterAutoSend(i, true);
        }

        // GetComponent<NetworkedTaskManager>().enabled = true;

        // CmdSpawnGenElements();

    //    GameObject generalElements = (GameObject) Instantiate()
    }


	public bool playerCanTransition ()
	{
		return inTransitionZone;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//if (other.tag == "TransitionZone") inTransitionZone = true;

        switch(other.tag)
        {
            case "Transition_Left":
                currentTransition = TransitionType.LEFT;
                break;
            case "Transition_Right":
                Debug.Log("Right Transition");
                currentTransition = TransitionType.RIGHT;
                break;
            case "Transition_UpLeft":
                currentTransition = TransitionType.UP_LEFT;
                break;
            case "Transition_UpRight":
                currentTransition = TransitionType.UP_RIGHT;
                break;
            case "Transition_DownLeft":
                currentTransition = TransitionType.DOWN_LEFT;
                break;
            case "Transition_DownRight":
                currentTransition = TransitionType.DOWN_RIGHT;
                break;
        }
	}

	void OnTriggerExit2D(Collider2D other)
	{
        //if (other.tag == "TransitionZone") inTransitionZone = false;

        if (other.tag.Contains("Transition")) currentTransition = TransitionType.NONE;
    }

    // [Command]
    // public void CmdSpawnGenElements()
    // {
    //     NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

    //     GameObject genElements = Instantiate(networkManager.spawnPrefabs[0], transform.position, Quaternion.identity) as GameObject;

    //     NetworkServer.SpawnWithClientAuthority(genElements, gameObject);

    // //    gameObject.GetComponent<NetworkedRoundManager>().AssignReadyButton(genElements.transform.GetChild(1).gameObject);

    //     if (hasAuthority)
    //     {
    //         gameObject.GetComponent<NetworkedRoundManager>().AssignReadyButton(genElements.transform.GetChild(1).gameObject);
    //     }
       
    //    if (!hasAuthority)
    //    {
    //        genElements.transform.GetChild(1).Translate(0, -200f, 0);
    //    }
    // }

    // [Command]
    // public void CmdSpawnMiniGameCanvas()
    // {

    // }
}
