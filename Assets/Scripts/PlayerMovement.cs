using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool smoothTransition = false;
    public float transitionSpeed = 10f;
    public float transitionRotationSpeed = 500f;

    Vector3 targetGridPos;
    Vector3 prevTargetGridPos;
    Vector3 targetRotation;

    public void RotationLeft() { if (AtRest) targetRotation -= Vector3.up * 90f; }
    public void RotationRight() { if (AtRest) targetRotation += Vector3.up * 90f; }
    public void MoveForward() { if (AtRest) targetGridPos += transform.forward; }
    public void MoveBackward() { if (AtRest) targetGridPos -= transform.forward; }
    public void MoveLeft() { if (AtRest) targetGridPos -= transform.right; }
    public void MoveRight() {  if (AtRest) targetGridPos += transform.right; }

    bool AtRest
    {
        get
        {
            if ((Vector3.Distance(transform.position, targetGridPos) > 0.05f) &&
                (Vector3.Distance(transform.eulerAngles, targetRotation) < 0.05f))
                return true;
            else
                return false;
        }
    }
}
