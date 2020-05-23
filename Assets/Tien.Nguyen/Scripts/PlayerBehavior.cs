using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;

    GameManager gameManager;

    public float speed = 0.5f;
    public float fastSpeed;
    public float lowSpeed = 0.1f;
    public float rotateSpeed = 30;

    Vector3 moveDir = Vector3.zero;

    public float gravity = 99f;
    public bool _isGrounded = true;
    private Transform _groundChecker;
    public float GroundDistance = 0.2f;
    public LayerMask Ground;

    [SerializeField] private bool isCanSelect;
    [SerializeField] private int indexOfButton;

    [Space]
    public SingleJoystick singleJoystick;
    Vector3 input01;

    private void Start()
    {
        gameManager = GameManager.GLOBAL;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        //isDead = false;
        _groundChecker = transform.GetChild(0);
        fastSpeed = speed;
    }

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (isCanSelect)
        //    {
        //        gameManager.SelectAnswer(indexOfButton);
        //    }
        //}
    }

    private void FixedUpdate()
    {
        MovementCharacter();
    }

    void MovementCharacter()
    {
        // get input from both joysticks
        input01 = singleJoystick.GetInputDirection();

        float xMovementInput01 = input01.x; // The horizontal movement from joystick 01
        float zMovementInput01 = input01.y; // The vertical movement from joystick 01

        float tempAngle = Mathf.Atan2(zMovementInput01, xMovementInput01);
        xMovementInput01 *= Mathf.Abs(Mathf.Cos(tempAngle));
        zMovementInput01 *= Mathf.Abs(Mathf.Sin(tempAngle));
        input01 = new Vector3(xMovementInput01, 0, zMovementInput01);

        moveDir.z = Input.GetAxis("Vertical");
        moveDir.x = Input.GetAxis("Horizontal");

        if (input01.z != 0 || input01.x != 0)
        {
            //moveDir = new Vector3(moveDir.x, 0, moveDir.z);

            //if (_isGrounded == false)
            //{
            //    moveDir.y -= gravity;
            //}

            //rb.MovePosition(transform.position + moveDir * speed * Time.fixedDeltaTime);

            //var newRotation = Quaternion.LookRotation(moveDir);
            //newRotation.x = 0f;
            //newRotation.z = 0f;
            //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);


            input01 = new Vector3(-xMovementInput01, 0, zMovementInput01);
            input01 = transform.TransformDirection(input01);
            input01 *= speed;

            // Make rotation object(The child object that contains animation) rotate to direction we are moving in.
            Vector3 temp = transform.position;
            temp.x += xMovementInput01;
            temp.z += zMovementInput01;
            Vector3 lookingVector = temp - transform.position;
            if (lookingVector != Vector3.zero)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(lookingVector), rotateSpeed * Time.deltaTime);
            }

            rb.transform.Translate(input01 * Time.fixedDeltaTime);

            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }
    }

    public void SelectInMobile()
    {
        if (isCanSelect)
        {
            gameManager.SelectAnswer(indexOfButton);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isCanSelect = true;
        indexOfButton = other.GetComponent<ButtonBehavior>().IndexNumber;
    }

    private void OnTriggerExit(Collider other)
    {
        isCanSelect = false;
        indexOfButton = -1;
    }
}
