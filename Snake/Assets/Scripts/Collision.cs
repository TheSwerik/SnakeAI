using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Apple"))
        {
            Debug.Log("EAT APPLE");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Snake"))
        {
            Debug.Log("DIE");
            Destroy(transform.parent.gameObject);
        }
    }
}