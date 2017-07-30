using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public Transform playerTransform;
	public Rigidbody2D playerRigidbody;

	float horizontalInput;
	float verticalInput;

	public bool movementDisabled;

	void Start () 
	{
		
		
		playerTransform = transform;

		horizontalInput = 0;
		verticalInput = 0;

		movementDisabled = false;
	}
	
	void Update () 
	{
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
	}
}
