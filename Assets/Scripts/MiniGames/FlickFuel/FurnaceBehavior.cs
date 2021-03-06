﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceBehavior : MonoBehaviour
{
    public static FurnaceBehavior FurnaceBehaviorReference;

    public IEnumerator movementCoroutine;

    public float moveDuration;
    float currentMoveTime = 0;

    public Vector3 rightPosition;
    public Vector3 leftPosition;

    public bool doMove;
    public bool moveLeft = false;
    public bool moveRight = false;

    // private BoxCollider2D furnaceTrigger;
    // public FurnaceCollisionCheck leftCheck, rightCheck, topCheck, bottomCheck;

    void Start ()
    {
        FurnaceBehaviorReference = this;
        // furnaceTrigger = GetComponent<BoxCollider2D>();
    }


    void Update ()
    {
        if (PlayerMiniGameHandler.IsInGame() && FlickFuel.GetDoStartGame())
        {
            doMove = true;
            StartCoroutine("MoveFurnace");
            FlickFuel.SetDoStartGame(false);

            // if (leftCheck.WasActivated() || rightCheck.WasActivated() || topCheck.WasActivated())
            // {
            //     furnaceTrigger.enabled = false;
            // }
            // else if (bottomCheck.WasActivated())
            // {
            //     furnaceTrigger.enabled = true;
            // }
        }

        if (moveLeft)
        {
            MoveFurnaceLeft();
        }

        if (moveRight)
        {
            MoveFurnaceRight();
        }
    }

    void MoveFurnaceLeft()
    {
        if (currentMoveTime < moveDuration) currentMoveTime += Time.deltaTime / moveDuration;

        transform.localPosition = Vector3.Lerp(rightPosition, leftPosition, currentMoveTime);

        if (transform.localPosition.x <= (leftPosition.x * 0.999f))
        {
            transform.localPosition = leftPosition;
            currentMoveTime = 0;
            moveLeft = false;
        }
    }

    void MoveFurnaceRight()
    {
        if (currentMoveTime < moveDuration) currentMoveTime += Time.deltaTime / moveDuration;

        transform.localPosition = Vector3.Lerp(leftPosition, rightPosition, currentMoveTime);

        if (transform.localPosition.x >= (rightPosition.x * 0.999f))
        {
            transform.localPosition = rightPosition;
            currentMoveTime = 0;
            moveRight = false;
        }
    }


    IEnumerator MoveFurnace()
    {
        while (doMove)
        {
            moveLeft = true;
            yield return new WaitForSeconds(moveDuration);

            moveRight = true;
            yield return new WaitForSeconds(moveDuration);
        }
    }

    public void StopFurnaceMovement()
    {
        currentMoveTime = 0;
        doMove = false;
        moveLeft = false;
        moveRight = false;
        StopCoroutine("MoveFurnace");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //8 is "Flickable"
        if (other.gameObject.layer == 8)
        {
            FlickFuel.FlickFuelReference.CaughtCoal();

            other.GetComponent<CoalBehavior>().HideCoal();
        }
    }
}
