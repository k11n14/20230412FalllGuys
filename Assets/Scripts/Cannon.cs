using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject cannonBallPrefab;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float angleRange = 60f;



    private Transform body;
    private float angleValue;

    //[SerializeField] private float cannonPower = 1000f;

    //private Rigidbody cannonBallRb;


    // Start is called before the first frame update
    void Start()
    {
        //Shot(20f);
        body = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        //angleValue += 3f * Time.deltaTime;
        float cycle = Mathf.Sin(Time.time * rotationSpeed);

        angleValue = cycle * angleRange;


        //ようわからん
        body.localRotation = Quaternion.AngleAxis(angleValue, Vector3.up);
    }

    public void Shot(float power)
    {
        //Instantiate(インスタンス元のオブジェクト, 生成時のPosition, 生成時のRotation);
        //Quaternion.identityで回転していない状態
        var cannonBall = Instantiate(cannonBallPrefab, muzzle.position, Quaternion.identity);
        var cannonBallRb = cannonBall.GetComponent<Rigidbody>();
        cannonBallRb.AddForce(muzzle.forward * power, ForceMode.VelocityChange);

   
    }
}
