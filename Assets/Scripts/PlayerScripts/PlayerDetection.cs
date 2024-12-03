using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class RaycastDetection : MonoBehaviour
{
    //range of raycast
    public float detectRange = 8f;
    //direction of raycast
    public Vector3 raycastForward = Vector3.forward;
    public Vector3 raycastBackwards = Vector3.back;
    public Vector3 raycastLeft = Vector3.left;
    public Vector3 raycastRight = Vector3.right;
    //Can Attack
    public bool canAttack = false;

    public GameObject enemyBehind;
    public GameObject enemyLeft;
    public GameObject enemyRight;
    public GameObject canAttackOptions;
    public GameObject attackMelee;
    public GameObject attackRange;
    public GameObject endTurn;
    public float enemyDist;

    void Update()
    {
        if (ForwardRay())
        {
            canAttack = true;
            canAttackOptions.SetActive(canAttack);
            endTurn.SetActive(canAttack);
            if(enemyDist > 1 && enemyDist< 5)
            {
                attackMelee.SetActive(true);
                attackRange.SetActive(false);

            }
            else if (enemyDist > 5 && enemyDist < 10)
            {
                attackRange.SetActive(true);
                attackMelee.SetActive(false);
            }
        }
        else
        {
            attackMelee.SetActive(false);
            attackRange.SetActive(false);

            canAttack = false;
            canAttackOptions.SetActive(canAttack);
            endTurn.SetActive(canAttack);
        }
        if (BackwardRay())
        {
            enemyBehind.SetActive(true);
        }
        else
        {
            enemyBehind.SetActive(false);            
        }
        if (LeftRay())
        {
            enemyLeft.SetActive(true);
        }
        else
        {
            enemyLeft.SetActive(false);
        }
        if (RightRay())
        {
            enemyRight.SetActive(true);
        }
        else
        {
            enemyRight.SetActive(false);
        }
    }

    bool ForwardRay()
    {
        // Cast a ray from the player's position
        Ray Forward = new Ray(transform.position, transform.TransformDirection(raycastForward));
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(raycastForward) * detectRange, Color.blue);
        // Perform the raycast
        if (Physics.Raycast(Forward, out hit, detectRange))
        {
            // Check if the object hit is tagged as "Enemy"
            if (hit.collider.CompareTag("Enemy"))
            {
                enemyDist = hit.distance;
                //Debug.Log("Enemy detected: Can Attack");
                return true;
            }
            else
            {
                enemyDist = 0;
                return false;
            }
        }
        enemyDist = 0;
        return false;
    }
    bool BackwardRay()
    {
        // Cast a ray from the player's position
        Ray Backward = new Ray(transform.position, transform.TransformDirection(raycastBackwards));
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(raycastBackwards) * detectRange, Color.green);
        // Perform the raycast
        if (Physics.Raycast(Backward, out hit, detectRange))
        {
            // Check if the object hit is tagged as "Enemy"
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy detected Behind");
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    bool LeftRay()
    {
        // Cast a ray from the player's position
        Ray Left = new Ray(transform.position, transform.TransformDirection(raycastLeft));
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(raycastLeft) * detectRange, Color.yellow);
        // Perform the raycast
        if (Physics.Raycast(Left, out hit, detectRange))
        {
            // Check if the object hit is tagged as "Enemy"
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy detected Left");
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    bool RightRay()
    {
        // Cast a ray from the player's position
        Ray Right = new Ray(transform.position, transform.TransformDirection(raycastRight));
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(raycastRight) * detectRange, Color.red);
        // Perform the raycast
        if (Physics.Raycast(Right, out hit, detectRange))
        {
            // Check if the object hit is tagged as "Enemy"
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy detected Right");
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}

