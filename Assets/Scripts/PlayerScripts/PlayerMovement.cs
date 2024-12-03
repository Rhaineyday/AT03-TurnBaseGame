using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    // Why: These variables track where the object should move and how it should rotate, without changing its position and rotation until later.
    //targetPosition: This Vector3 will store the target position of the object.
    [SerializeField] Vector3 targetPosition;
    //targetRotation: This Vector3 will store the target rotation in Euler angles (pitch, yaw, roll).
    [SerializeField] Vector3 targetRotation;
    public Text actionPointDisplay;
    public float unit = 4;
    public int actionsInTurn = 3;
    public LayerMask dontIgnore;
    #endregion
    #region Unity Event Functions
    //Why: LateUpdate is used here because you may want to set the final position and rotation of an object after all other logic and physics calculations have been made. This ensures smooth, predictable movement and rotation.
    //LateUpdate: This Unity method is called once per frame after all Update methods have been executed. It is typically used to adjust things like camera position or object transformations, after all other logic has been processed.
    private void Start()
    {
        StartPlayerTurn();
    }
    public void StartPlayerTurn()
    {
        actionsInTurn = 3;
        UpdateActionPoints(0);
        GameManager.instance.state = GameStates.PlayerTurn;
    }
    private void LateUpdate()
    {
        //transform.position = targetPosition;: This sets the position of the GameObject to the targetPosition. The transform.position property represents the object's position in world space.
        transform.position = targetPosition;
        //transform.rotation = Quaternion.Euler(targetRotation);: This sets the GameObject’s rotation based on the targetRotation vector. Quaternion.Euler() converts the Euler angles (pitch, yaw, roll) from targetRotation into a Quaternion, which Unity uses for 3D rotations.
        transform.rotation = Quaternion.Euler(targetRotation);
        this.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }
    #endregion
    #region Actions
    //public void Forward(): This is a public method that can be called to move the object forward.
    //Why: This method allows the object to "move forward" by changing its target position in the direction it's facing.
    public void Forward()
    {
        if (GameManager.instance.state == GameStates.PlayerTurn)
        {
            Ray line = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(line, out hitInfo, 4, dontIgnore))
            {
                Debug.Log("I CANT MOVE DUDE SOMETHING IS THERE");
                return;
            }
            else if (actionsInTurn > 0)
            {
                //targetPosition += transform.forward;: The transform.forward is a unit vector (length of 1) pointing in the direction the GameObject is facing. When you add it to targetPosition, you move the object one unit forward in its current direction.
                targetPosition += transform.forward * unit;
                UpdateActionPoints(1);
            }
        }
    }
    //Why: This method allows the object to rotate left (counter-clockwise) by 90 degrees on the Y-axis, updating the target rotation.
    //public void TurnLeft90(): This public method rotates the object 90 degrees to the left (counter-clockwise around the Y-axis).
    public void TurnLeft90()
    {
        if (GameManager.instance.state == GameStates.PlayerTurn)
        {
            if (actionsInTurn > 0)
            {
                //targetRotation -= Vector3.up * 90f;: Vector3.up represents the unit vector (0, 1, 0), which is the Y-axis. Multiplying it by 90 degrees gives a vector that represents a 90-degree rotation around the Y-axis. By subtracting this from targetRotation, you are rotating the object 90 degrees counter-clockwise.
                targetRotation -= Vector3.up * 90f;
                UpdateActionPoints(1);
            }
        }
    }
    //public void TurnRight90(): This method rotates the object 90 degrees to the right (clockwise around the Y-axis).
    //Why: This method allows the object to rotate right (clockwise) by 90 degrees on the Y-axis, updating the target rotation.
    public void TurnRight90()
    {
        if (GameManager.instance.state == GameStates.PlayerTurn)
        {
            if (actionsInTurn > 0)
            {

                //targetRotation += Vector3.up * 90f;: Here, you add 90 degrees to the targetRotation vector. This causes the object to rotate clockwise around the Y-axis.
                targetRotation += Vector3.up * 90f;
                UpdateActionPoints(1);
            }
        }
    }
    //public void Turn180(): This method rotates the object by 180 degrees, turning it around the Y-axis.
    //Why: This allows you to turn the object around by 180 degrees, which is useful for making it face the opposite direction.
    public void Turn180()
    {
        if (GameManager.instance.state == GameStates.PlayerTurn)
        {
            if (actionsInTurn > 1)
            {

                //targetRotation -= Vector3.up * 180f;: By subtracting 180 degrees from the targetRotation, you rotate the object 180 degrees around the Y-axis.
                targetRotation -= Vector3.up * 180f;
                UpdateActionPoints(2);
            }
        }
    }
    public void EndTurn()
    {
        UpdateActionPoints(actionsInTurn);
    }
    public void UpdateActionPoints(int value)
    {
        actionsInTurn -= value;
        if (actionsInTurn <= 0)
        {
            Debug.Log("Action point is now zero");
            // Change to the enemy's turn...
            GameManager.instance.state = GameStates.EnemyTurn;
        }
        actionPointDisplay.text = $"Action Points: {actionsInTurn}";
    }
    #endregion
}
