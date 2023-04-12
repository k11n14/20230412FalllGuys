using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    [SerializeField] private float waittingTime = 3f;
    [SerializeField] private float initialaizeTime = 5f;
    [SerializeField] private Color warningColor = Color.red; 


    Rigidbody rb;

    private Vector3 initalPosition;
    private Quaternion initalRotation;
    private Color initalColor;
    private MeshRenderer floorMeshRender;

    // Start is called before the first frame update
     void Start()
    {
        rb = GetComponent<Rigidbody>();

        initalPosition = transform.position;
        initalRotation = transform.rotation;
        floorMeshRender = transform.GetChild(0).GetComponent<MeshRenderer>();
        initalColor = floorMeshRender.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
        floorMeshRender.material.color = warningColor;
      
        //Debug.Log("衝突した！");
        //rb.isKinematic = false;
        //数秒後に実行するような処理にはコルーチン(Coroutine)という機能を使う
        StartCoroutine(Fall());
        }

    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(waittingTime);
        rb.isKinematic = false;

        yield return new WaitForSeconds(initialaizeTime);
        Init();
    }

    private void Init()
    {
        rb.isKinematic = true;
        transform.position = initalPosition;
        transform.rotation = initalRotation;
        floorMeshRender.material.color = initalColor;

    }
}
