using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{   
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
