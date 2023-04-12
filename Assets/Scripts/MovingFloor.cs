using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    //[SerializeField] private Transform F;
    private Transform body;

    [SerializeField] private float movingSpeed;
    [SerializeField] private float movingRange;
    [SerializeField] float offset; 


    // Start is called before the first frame update
   private void Start()
    {
        //F.position = transform.position
        body = transform.GetChild(0);
        Debug.Log(body.name);

    }

    // Update is called once per frame
   private void FixedUpdate()
    {
        //ある特定の＋ーの範囲を行き来する
        var cycle = Mathf.Sin(Time.time * movingSpeed+offset);


        body.localPosition = new Vector3(cycle * movingRange, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(body.transform);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
