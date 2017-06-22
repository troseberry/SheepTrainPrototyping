using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalBehavior : MonoBehaviour
{
    public Vector3 startingPosition;

    public void ShowCoal()
    {
        gameObject.SetActive(true);
    }


    public void HideCoal()
    {
        gameObject.SetActive(false);
    }

    public void ResetPosition()
    {
        transform.localPosition = startingPosition;
    }
}
