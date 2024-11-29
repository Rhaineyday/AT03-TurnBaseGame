using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    [SerializeField] Vector3 targetPosition;
    [SerializeField] Vector3 targetRotation;

    public float detectRange = 8f;

    public Vector3 raycastForward = Vector3.forward;
    public Vector3 raycastBackwards = Vector3.back;
    public Vector3 raycastLeft = Vector3.left;
    public Vector3 raycastRight = Vector3.right;
    //Can Attack
    public bool canAttack = false;

    //public GameObject playerBehind;
    //public GameObject playerLeft;
    //public GameObject playerRight;
    //public GameObject canAttackOptions;
    //public GameObject attackMelee;
    //public GameObject attackRange;

    public float playerDist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    transform.position = targetPosition;
    //    transform.rotation = Quaternion.Euler(targetRotation);
    //    this.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    //}

    void Update()
    {
        if (ForwardRay())
        {
            canAttack = true;
            //canAttackOptions.SetActive(canAttack);
            if (playerDist > 1 && playerDist < 5)
            {
                Debug.Log("Melee - Right in-front");
                //attackMelee.SetActive(true);
                //attackRange.SetActive(false);

            }
            else if (playerDist > 5 && playerDist < 10)
            {
                Debug.Log("Range - Far way in-front");
                //attackRange.SetActive(true);
                //attackMelee.SetActive(false);
            }
        }
        else
        {
            //attackMelee.SetActive(false);
            //attackRange.SetActive(false);

            canAttack = false;
            //canAttackOptions.SetActive(canAttack);
        }
        if (BackwardRay())
        {
            Debug.Log("Back ray");
            //playerBehind.SetActive(true);
        }
        //else
        //{
        //    //playerBehind.SetActive(false);
        //}
        if (LeftRay())
        {
            Debug.Log("Left ray");
            //playerLeft.SetActive(true);
        }
        //else
        //{
        //    playerLeft.SetActive(false);
        //}
        if (RightRay())
        {
            Debug.Log("Right ray");

            //playerRight.SetActive(true);
        }
        //else
        //{
        //    playerRight.SetActive(false);
        //}
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
            // Check if the object hit is tagged as "Player"
            if (hit.collider.CompareTag("Player"))
            {
                playerDist = hit.distance;
                Debug.Log("Player detected: Can Attack");
                return true;
            }
            else
            {
                playerDist = 0;
                return false;
            }
        }
        playerDist = 0;
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
            // Check if the object hit is tagged as "Player"
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player detected Behind");
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
            // Check if the object hit is tagged as "Player"
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player detected Left");
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
            // Check if the object hit is tagged as "Player"
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player detected Right");
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
