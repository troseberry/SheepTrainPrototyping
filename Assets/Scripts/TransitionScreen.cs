using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour 
{
    public static TransitionScreen TransitionScreenReference;

	private float transitionDuration = 0.5f;
    private float carDistance = 25f;
	private Vector3 updatedCameraPos;

    public Transform secondLeftCar;
    public Transform secondRightCar;

    private bool isMoving;

    //use these two when working in non-networked scene (minigame prototyping)
	public PlayerMovement playerMovement;
	public PlayerBehavior playerBehavior;

    //should be private when working in networked scene
    public Transform playerTarget;

    private NetworkedPlayerMovement networkedPlayerMovement;
	private NetworkedPlayerBehavior networkedPlayerBehavior;

    public Image leftArrow;
    public Image rightArrow;

    public Color opaqueArrow;
    public Color transparentArrow;

    void Start()
    {
        TransitionScreenReference = this;
    }

    public void SetCameraRight()
	{
        if (!isMoving && HasRoomToMove(1))
        {
            updatedCameraPos = new Vector3(transform.position.x + carDistance, transform.position.y, transform.position.z);
            isMoving = true;

            StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x - 5, -1));
        }
    }

	public void SetCameraLeft()
	{
        if (!isMoving && HasRoomToMove(-1))
        {
            updatedCameraPos = new Vector3(transform.position.x - carDistance, transform.position.y, transform.position.z);
            isMoving = true;

            StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x + 5, -1));
        }
    }


	IEnumerator MoveCamera(Vector3 targetPos, float playerHorz, float playerVert)
	{
        leftArrow.color = transparentArrow;
        rightArrow.color = transparentArrow;
        
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

        yield return new WaitForSeconds(0.35f);

        isMoving = false;
        leftArrow.color = opaqueArrow;
        rightArrow.color = opaqueArrow;
	}

    public void SetPlayerTarget(Transform player)
    {
        playerTarget = player;
        networkedPlayerMovement = playerTarget.GetComponent<NetworkedPlayerMovement>();
        networkedPlayerBehavior = playerTarget.GetComponent<NetworkedPlayerBehavior>();
    }

    private bool HasRoomToMove(int direction)
    {
        if (direction == -1)
        {
            return transform.position.x >= secondLeftCar.position.x;
        }
        else if (direction == 1)
        {
            return transform.position.x <= secondRightCar.position.x;
        }
        return false;
    }

    private void HandleArrowOpacity()
    {

    }
}
