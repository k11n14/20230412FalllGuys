using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    [SerializeField] private Cannon[] cannons;
    [SerializeField] private float power;

    [SerializeField] float shotMinInterval;
    [SerializeField] float shotMaxInterval;



    private bool isWaitingToShot;


    private void OnTriggerStay(Collider other)
    {
        if (isWaitingToShot) return;

        if (other.CompareTag("Player"))
        {
            //Debug.Log("侵入した");

            isWaitingToShot = true;
            StartCoroutine(ReadyToShot());
        }
    }

    IEnumerator ReadyToShot()
    {
        float waitingtime = Random.Range(shotMinInterval, shotMaxInterval);

        int cannonIndex = Random.Range(0, cannons.Length);

        yield return new WaitForSeconds(waitingtime);

        cannons[cannonIndex].Shot(power);

        isWaitingToShot = false;
    }
}
