using UnityEngine;
using System.Collections;

public class TransitionScreen : MonoBehaviour 
{
    public static TransitionScreen TransitionScreenReference;

	float transitionDuration = 0.5f;
    float carDistance = 25f;
	Vector3 updatedCameraPos;

    //use these two when working in non-networked scene (minigame prototyping)
	public PlayerMovement playerMovement;
	public PlayerBehavior playerBehavior;

    //should be private when working in networked scene
    public Transform playerTarget;

    private NetworkedPlayerMovement networkedPlayerMovement;
	private NetworkedPlayerBehavior networkedPlayerBehavior;

    public GameObject[] backgroundObjects;

    void Start()
    {
        TransitionScreenReference = this;
    }

    public void SetCameraRight()
	{
        updatedCameraPos = new Vector3(transform.position.x + carDistance, transform.position.y, transform.position.z);
        StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x - 5, -1));
    }

	public void SetCameraLeft()
	{
        updatedCameraPos = new Vector3(transform.position.x - carDistance, transform.position.y, transform.position.z);
        StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x + 5, -1));
    }

	// public void SetCameraUpRight()
	// {
    //     updatedCameraPos = new Vector3(transform.position.x + carDistance, transform.position.y + 8.5f, transform.position.z);
    //     StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x - 5, updatedCameraPos.y - 3.35f));
    // }

    // public void SetCameraUpLeft()
    // {
    //     updatedCameraPos = new Vector3(transform.position.x - carDistance, transform.position.y + 8.5f, transform.position.z);
    //     StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x + 5, updatedCameraPos.y - 3.35f));
    // }

    // public void SetCameraDownLeft()
	// {
    //     updatedCameraPos = new Vector3(transform.position.x - carDistance, transform.position.y - 8.5f, transform.position.z);
    //     StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x + 5, updatedCameraPos.y + 3.5f));
    // }

    // public void SetCameraDownRight()
    // {
    //     updatedCameraPos = new Vector3(transform.position.x + carDistance, transform.position.y - 8.5f, transform.position.z);
    //     StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x - 5, updatedCameraPos.y + 3.5f));
    // }


	IEnumerator MoveCamera(Vector3 targetPos, float playerHorz, float playerVert) //add player vert as argument
	{
		float t = 0.0f;
		Vector3 startingPos = transform.position;
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / transitionDuration);

			transform.position = Vector3.Lerp(startingPos, targetPos, t);

			yield return 0;
		}
		
        if (!networkedPlayerMovement)
		{
            playerMovement.playerTransform.position = new Vector3(playerHorz, playerVert, playerMovement.playerTransform.position.z);
		    playerMovement.movementDisabled = false;
        }
        else
        {
            networkedPlayerMovement.playerTransform.position = new Vector3(playerHorz, playerVert, networkedPlayerMovement.playerTransform.position.z);
		    networkedPlayerMovement.movementDisabled = false;
        }
	}

    public void SetPlayerTarget(Transform player)
    {
        playerTarget = player;
        networkedPlayerMovement = playerTarget.GetComponent<NetworkedPlayerMovement>();
        networkedPlayerBehavior = playerTarget.GetComponent<NetworkedPlayerBehavior>();
    }
}
