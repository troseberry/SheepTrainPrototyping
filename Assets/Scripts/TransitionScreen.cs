﻿using UnityEngine;
using System.Collections;

public class TransitionScreen : MonoBehaviour 
{
	float transitionDuration = 0.5f;
	Vector3 updatedCameraPos;

	public PlayerMovement playerMovement;
	public PlayerBehavior playerBehavior;

    void Start () 
	{

	}
	
	void Update () 
	{
	
	}


    public void SetCameraRight()
	{
        //if (playerBehavior.playerCanTransition())
        //{
        //    playerMovement.movementDisabled = true;
        //    StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x - 7, 0));
        //}
        updatedCameraPos = new Vector3(transform.position.x + 18, transform.position.y, transform.position.z);
        StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x - 7, 0));
    }

	public void SetCameraLeft()
	{
        //if (playerBehavior.playerCanTransition())
        //{
        //    playerMovement.movementDisabled = true;
        //    StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x + 7, 0));
        //}
        updatedCameraPos = new Vector3(transform.position.x - 18, transform.position.y, transform.position.z);
        StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x + 7, 0));
    }

	public void SetCameraUpRight()
	{
        //if (playerBehavior.playerCanTransition())
        //{
        //    updatedCameraPos = new Vector3(transform.position.x + 18, transform.position.y + 9, transform.position.z);
        //    StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x - 5, updatedCameraPos.y - 3.35f));
        //}
        updatedCameraPos = new Vector3(transform.position.x + 18, transform.position.y + 8.5f, transform.position.z);
        StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x - 5, updatedCameraPos.y - 3.35f));
    }

    public void SetCameraUpLeft()
    {
        //if (playerBehavior.playerCanTransition())
        //{
        //    updatedCameraPos = new Vector3(transform.position.x - 18, transform.position.y + 9, transform.position.z);
        //    StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x + 5, updatedCameraPos.y - 3.35f));
        //}
        updatedCameraPos = new Vector3(transform.position.x - 18, transform.position.y + 8.5f, transform.position.z);
        StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x + 5, updatedCameraPos.y - 3.35f));
    }

    public void SetCameraDownLeft()
	{
        //if (playerBehavior.playerCanTransition())
        //{
        //    updatedCameraPos = new Vector3(transform.position.x - 18, transform.position.y - 9, transform.position.z);
        //    StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x + 5, updatedCameraPos.y + 3.5f));
        //}
        updatedCameraPos = new Vector3(transform.position.x - 18, transform.position.y - 8.5f, transform.position.z);
        StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x + 5, updatedCameraPos.y + 3.5f));
    }

    public void SetCameraDownRight()
    {
        //if (playerBehavior.playerCanTransition())
        //{
        //    updatedCameraPos = new Vector3(transform.position.x + 18, transform.position.y - 9, transform.position.z);
        //    StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x - 5, updatedCameraPos.y + 3.5f));
        //}
        updatedCameraPos = new Vector3(transform.position.x + 18, transform.position.y - 8.5f, transform.position.z);
        StartCoroutine(MoveCamera(updatedCameraPos, updatedCameraPos.x - 5, updatedCameraPos.y + 3.5f));
    }





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
		
		playerMovement.playerTransform.position = new Vector3(playerHorz, playerVert, playerMovement.playerTransform.position.z);
		playerMovement.movementDisabled = false;
	}
}
