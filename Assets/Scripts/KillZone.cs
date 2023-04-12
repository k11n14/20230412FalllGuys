using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] Transform spawPoint;

    private void OnTriggerEnter(Collider other)
    {
         if(other.CompareTag("Player"))
        {
            other.transform.position = spawPoint.position;
        }
    }

}