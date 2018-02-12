using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour 
{
	public Transform playerTransform;
	public Rigidbody2D playerRigidbody;

	float horizontalInput;
	float verticalInput;
	Vector2 moveVector;

	private SpriteRenderer sheepSprite;
	private int previousFacingDirection = 1;
	private int facingDirection = 1;

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
		sheepSprite = GetComponent<SpriteRenderer>();

		horizontalInput = 0;
		verticalInput = 0;

		movementDisabled = false;

		pointerEvent = new PointerEventData(currentEventSystem);
		currentPlayerPosition = playerTransform.position;
		nextDestination = playerTransform.position;

	}
	
	void Update () 
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");

		if (!movementDisabled)
		{
			moveVector = new Vector2(horizontalInput, verticalInput);
			DebugPanel.Log("Move Vector", "Player", moveVector);
			playerTransform.Translate(moveVector * (4 * Time.deltaTime), Space.World);

			if (horizontalInput != 0 || verticalInput != 0)
			{
				PlayerAnimator.SetAnimState(AnimState.WALK);
			}
			else if (horizontalInput == 0 && verticalInput == 0)
			{
				PlayerAnimator.SetAnimState(AnimState.IDLE);
			}

			
			if (FacingDirectionChanged())
			{
				Debug.Log("Changed Facing Direction");
				sheepSprite.flipX = !sheepSprite.flipX;
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			nextDestination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			currentPlayerPosition = playerTransform.position;
			currentMoveTime = 0;
			doMove = true;
		}	

		if (Input.GetKeyDown(KeyCode.F)) { FlipPlayerSprite(); }	
	}

	void FlipPlayerSprite()
	{
		playerTransform.RotateAround(playerTransform.position, Vector3.up, 180f);
	}

	bool FacingDirectionChanged()
	{
		if (horizontalInput != 0)
		{
			previousFacingDirection = facingDirection;
			facingDirection = horizontalInput > 0 ? 1 : -1;
		}
		return facingDirection != previousFacingDirection;
	}
}
