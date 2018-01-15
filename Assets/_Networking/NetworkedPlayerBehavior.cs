using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkedPlayerBehavior : NetworkBehaviour 
{
    public enum TransitionType { LEFT, RIGHT, UP_LEFT, UP_RIGHT, DOWN_LEFT, DOWN_RIGHT, NONE };

    bool inTransitionZone;
    public TransitionType currentTransition;

	void Start () 
	{
        inTransitionZone = false;
        currentTransition = TransitionType.NONE;
	}

    public override void OnStartLocalPlayer()
    {
        TransitionScreen.TransitionScreenReference.SetPlayerTarget(transform);

        GetComponent<SpriteRenderer>().color = Color.red;
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
}
