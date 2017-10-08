using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class PlayerMovement :  NetworkBehaviour
{
	public Transform playerTransform;
	public Rigidbody2D playerRigidbody;

	float horizontalInput;
	float verticalInput;

	public bool movementDisabled;

	Vector2 nextDestination;
	Vector2 currentPlayerPosition;

	PointerEventData pointerEvent;
	public EventSystem currentEventSystem;

	bool doMove = false;
	public float moveDuration = 4.0f;
    float currentMoveTime = 0;


	void Start () 
	{
		
		playerTransform = transform;

		horizontalInput = 0;
		verticalInput = 0;

		movementDisabled = false;

		pointerEvent = new PointerEventData(currentEventSystem);
		currentPlayerPosition = playerTransform.position;
		nextDestination = playerTransform.position;

	}
	
	void Update () 
	{
        if (!isLocalPlayer)
        {
            return;
        }
        
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");

		if (!movementDisabled)
		{
			if (horizontalInput > 0) 
			{
				playerTransform.Translate(Vector3.right * (4 * Time.deltaTime));
			} 
			else if (horizontalInput < 0) 
			{
				playerTransform.Translate(Vector3.left * (4 * Time.deltaTime));
			}
			
			if (verticalInput > 0) 
			{
				playerTransform.Translate(Vector3.up * (4 * Time.deltaTime));
			} 
			else if (verticalInput < 0) 
			{
				playerTransform.Translate(Vector3.down * (4 * Time.deltaTime));
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			nextDestination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			currentPlayerPosition = playerTransform.position;
			currentMoveTime = 0;
			doMove = true;
		}
		DebugPanel.Log("Pointer Press: ", nextDestination);

		// if (doMove)
		// {
		// 	if (currentMoveTime < moveDuration) currentMoveTime += Time.deltaTime / moveDuration;
		// 	playerTransform = Vector2.Lerp(currentPlayerPosition, nextDestination);
		// }
		
	}
}
