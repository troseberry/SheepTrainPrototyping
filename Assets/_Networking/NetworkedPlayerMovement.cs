using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class NetworkedPlayerMovement : NetworkBehaviour 
{
	public Transform playerTransform;
	public Rigidbody2D playerRigidbody;

	float horizontalInput;
	float verticalInput;
	Vector2 moveVector;

	public SpriteRenderer sheepSprite;
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

		// Need for point 'n click movement
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

			
			// if (FacingDirectionChanged())
			// {
			// 	Debug.Log("Changed Facing Direction");
			// 	sheepSprite.flipX = !sheepSprite.flipX;
			// 	CmdProvideFlipToServer(true);
			// }
			Flip();
		}

		if (Input.GetMouseButtonDown(0))
		{
			nextDestination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			currentPlayerPosition = playerTransform.position;
			currentMoveTime = 0;
			doMove = true;
		}

		// if (doMove)
		// {
		// 	if (currentMoveTime < moveDuration) currentMoveTime += Time.deltaTime / moveDuration;
		// 	playerTransform = Vector2.Lerp(currentPlayerPosition, nextDestination);
		// }
	}

	// void FlipPlayerSprite()
	// {
	// 	playerTransform.RotateAround(playerTransform.position, Vector3.up, 180f);
	// }

	bool FacingDirectionChanged()
	{
		if (horizontalInput != 0)
		{
			previousFacingDirection = facingDirection;
			facingDirection = horizontalInput > 0 ? 1 : -1;
		}
		return facingDirection != previousFacingDirection;
	}

	[Command]
	void CmdProvideFlipToServer(bool stateChanged)
	{
		Debug.Log("Client Check 0.5");
		if (stateChanged)
		{
			Debug.Log("Client Check 1");
			sheepSprite.flipX = !sheepSprite.flipX;

			// GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
			// NetworkedPlayerMovement[] allCounts = new NetworkedPlayerMovement[allPlayers.Length];
			// for (int i = 0; i < allPlayers.Length; i++)
			// {
			// 	allCounts[i] = allPlayers[i].GetComponent<NetworkedPlayerMovement>();
			// 	allCounts[i].UpdateRemoteSpriteFlip();
			// }
			RpcSendFlipState(stateChanged);
		}
	}

	[ClientRpc]
	void RpcSendFlipState(bool stateChanged)
	{
		if (!isLocalPlayer) return;
		
		Debug.Log("Client Check 2");
		if (stateChanged)
		{
			Debug.Log("Client Check 2.5");
			sheepSprite.flipX = !sheepSprite.flipX;
		}
	}

	[Client]
	void Flip()
	{
		if (!isLocalPlayer) return;

		if (FacingDirectionChanged()) 
		{
			Debug.Log("Client Check 0");
			sheepSprite.flipX = !sheepSprite.flipX;
			CmdProvideFlipToServer(true);
		}
	}

	public void UpdateRemoteSpriteFlip()
	{
		sheepSprite.flipX = !sheepSprite.flipX;
	}
}
