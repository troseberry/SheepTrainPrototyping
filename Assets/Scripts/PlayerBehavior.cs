using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour 
{
    public enum TransitionType { LEFT, RIGHT, UP_LEFT, UP_RIGHT, DOWN_LEFT, DOWN_RIGHT, NONE };

    bool inTransitionZone;
    public TransitionType currentTransition;

	void Start () 
	{
        inTransitionZone = false;
        currentTransition = TransitionType.NONE;
	}
	
	void Update () 
	{
	
	}


	public bool playerCanTransition ()
	{
		return inTransitionZone;
	}

	void OnTriggerEnter(Collider other)
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

	void OnTriggerExit(Collider other)
	{
        //if (other.tag == "TransitionZone") inTransitionZone = false;

        if (other.tag.Contains("Transition")) currentTransition = TransitionType.NONE;
    }
}
