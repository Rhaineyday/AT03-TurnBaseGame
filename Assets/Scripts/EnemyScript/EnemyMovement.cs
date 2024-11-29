using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    #region Variables
    // Why: These variables track where the object should move and how it should rotate, without changing its position and rotation until later.
    //targetPosition: This Vector3 will store the target position of the object.
    Vector3 targetPosition;
    //targetRotation: This Vector3 will store the target rotation in Euler angles (pitch, yaw, roll).
    Vector3 targetRotation;
    //public Text actionPointDisplay;
    public Text enemyTurnText;
    public float unit = 4;
    public int actionsInTurn = 5;

    #endregion
    #region Unity Event Functions
    //Why: LateUpdate is used here because you may want to set the final position and rotation of an object after all other logic and physics calculations have been made. This ensures smooth, predictable movement and rotation.
    //LateUpdate: This Unity method is called once per frame after all Update methods have been executed. It is typically used to adjust things like camera position or object transformations, after all other logic has been processed.
    private void Start()
    {
        UpdateActionPoints(0);
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

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state == GameStates.EnemyTurn)
        {
            if (enemyTurnText.text != "Enemy Turn")
            {
                enemyTurnText.text = "Enemy Turn";
                Debug.Log("Enemy can move now!!!");
            }

        }
        else
        {
            if (enemyTurnText.text != "Player Turn")
            {
                enemyTurnText.text = "Player Turn";
                Debug.Log("Back to Player Turn");
            }
        }
    }



    void UpdateActionPoints(int value)
    {
        actionsInTurn -= value;
        if (actionsInTurn == 0)
        {
            Debug.Log("Action point is now zero");
            // Change to the enemy's turn...
            GameManager.instance.state = GameStates.PlayerTurn;
        }
        //actionPointDisplay.text = $"Action Points: {actionsInTurn}";
    }
}
