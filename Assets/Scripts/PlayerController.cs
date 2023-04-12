using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private Transform foot;

   


    private PlayerInput playerInput;
    private InputAction moveInput;
    private InputAction jumpInput;

    private CharacterController characterController;
    private Rigidbody rb;

    Camera cam;

    private Vector3 moveDirection;
    private float distanceToGround;
    private bool isGround;


    // Start is called before the first frame update
    private void Start()
    {
        //transform.position.x=1
        playerInput = GetComponent<PlayerInput>();
        moveInput = playerInput.actions["Move"];
        jumpInput = playerInput.actions["Jump"];

        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        distanceToGround = transform.position.y - foot.position.y + 0.1f;

        cam = Camera.main;

    }

    // Update is called once per frame
    private void Update()
    {
        //入力値の読み取り(-1<=x<=1,-1<=x<=1)
        Vector2 moveInputValue = moveInput.ReadValue<Vector2>();

        //Debug.Log("moveInputValue" + moveInputValue);

        //float horizontalInput = Input.GetAxis("Horizontal");
        //Debug.Log("horizontalInput"+horizontalInput);

        //Vector3 pos = transform.position;
        //pos.x += moveInputValue.x * Time.deltaTime* moveSpeed;
        //pos.z += moveInputValue.y * Time.deltaTime * moveSpeed;
        //transform.position = pos;

        //characterController.Move(Time.deltaTime * moveSpeed * new Vector3(moveInputValue.x, 0, moveInputValue.y));

        //rbに与える力の向きに変換
        //moveDirection = new Vector3(moveInputValue.x, 0, moveInputValue.y);
       

        //if (moveInputValue != Vector2.zero)
        if (moveDirection != Vector3.zero)
        {
            Ratate();
           
        }

        if(isGround && jumpInput.WasPressedThisFrame())
        {
            Jump();
        }

        isGround = CheackGround();

        //メインカメラの向いてる方向のVecter３
        Vector3 cameraForward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);

        //よくわからん
        //Vector3 verticalValue = moveInputValue.y * cam.transform.forward;

        //カメラの前後に対しての＋ー１
        Vector3 verticalValue = moveInputValue.y * cameraForward;

        //カメラの左右に対しての＋ー１
        Vector3 holizinalValue = moveInputValue.x * cam.transform.right;

        moveDirection = verticalValue + holizinalValue;
    }

    private void FixedUpdate()
    {
        Move();
       
    }


    //tureかfalseを判定して返す関数。
    bool CheackGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround);
    }

    private void Move()
    {
        //Vector3.magnitudeでベクトルの長さ（=速度）を取得できる
        float currentSpped = rb.velocity.magnitude;
        if (currentSpped > maxSpeed) return;
        rb.AddForce(moveDirection * moveSpeed, ForceMode.VelocityChange);
    }

    private void Ratate()
    {
        //Quaternion.LookRotation(Vector3 向かせる方向);
        //Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveInputValue.x, 0, moveInputValue.y));
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        //Debug.Log("targetRotation" + targetRotation);

        //transform.rotation = targetRotation;
        //Quaternion.Lerp(transform.rotation初期値から, targetRotation変更値まで, rotationSpeed * Time.deltaTime速度);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);

    }
}

