using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingSheepBehavior : MonoBehaviour
{
    Rigidbody2D sheepRigidbody;

    Vector2 startingPosition = new Vector2(-1050, 0);
    Vector2 endingPosition = new Vector2(1050, 0);

    float currentMoveTime = 0;
    float moveDuration = 3.0f;

    bool doJump = false;

    void Start ()
    {
        sheepRigidbody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "SheepCatcher")
        {
            gameObject.SetActive(false);
        }
        else if (other.gameObject.name == "Passenger")
        {
            gameObject.SetActive(false);
            SheepJump.SheepJumpReference.failedEarly = true;
        }
    }

    void FixedUpdate()
    {
        if (!SheepJump.SheepJumpReference.failedEarly)
        {
            sheepRigidbody.velocity = new Vector2(6.5f, sheepRigidbody.velocity.y);

            if (doJump)
            {
                sheepRigidbody.AddForce(new Vector2(-8, 10), ForceMode2D.Impulse);
                doJump = false;
            }
        }
    }

    public void ExecuteJump()
    {
        doJump = true;
    }

    public void ResetPosition()
    {
        transform.localPosition = startingPosition;
        currentMoveTime = 0;
    }
}
