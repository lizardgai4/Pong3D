using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBounceScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit detected");
    }
}
