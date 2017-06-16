using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateValve : MonoBehaviour {

    public const float SPEED_DAMPER = 0.01f;

    Vector2 previousMousePosition = Vector2.zero;
    Vector2 currentMousePosition = Vector2.zero;
    bool previousFrameMouseDown = false;

    public static float rotationAmount = 0;


    private void Update()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x - Screen.width / 2,
                                       Input.mousePosition.y - Screen.height / 2);

        if (Input.GetMouseButtonDown(0) && !previousFrameMouseDown)
        {
            previousMousePosition = mousePos;
            currentMousePosition = mousePos;
            previousFrameMouseDown = true;
        }
        else if (Input.GetMouseButton(0) && previousFrameMouseDown)
        {
            previousMousePosition = currentMousePosition;
            currentMousePosition = mousePos;
        }
        else if (!Input.GetMouseButton(0))
        {
            previousFrameMouseDown = false;
        }

        if (previousMousePosition != -currentMousePosition && previousFrameMouseDown)
        {
            rotationAmount = ReturnSignedAngleBetweenVectors(previousMousePosition, currentMousePosition);
            transform.RotateAroundLocal(Vector3.forward, rotationAmount * SPEED_DAMPER);

            DebugPanel.Log("Rotation Amount: ", rotationAmount);
            //positive value: counterclockwise rot
            //negative value: clockwise rot
        }
    }


    private float ReturnSignedAngleBetweenVectors(Vector2 vectorA, Vector2 vectorB)
    {
        Vector3 vector3A = new Vector3(vectorA.x, vectorA.y, 0f);
        Vector3 vector3B = new Vector3(vectorB.x, vectorB.y, 0f);

        if (vector3A == vector3B)
            return 0f;

        // refVector is a 90cw rotation of vector3A
        Vector3 refVector = Vector3.Cross(vector3A, Vector3.forward);
        float dotProduct = Vector3.Dot(refVector, vector3B);

        if (dotProduct > 0)
            return -Vector3.Angle(vector3A, vector3B);
        else if (dotProduct < 0)
            return Vector3.Angle(vector3A, vector3B);
        else
            throw new System.InvalidOperationException("the vectors are opposite, vectorA = -vectorB");
    }
}
