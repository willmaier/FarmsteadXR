using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float jumpAmount = 1.0f;
    public float gravity = 9.81f;
    public float airControl = 10.0f;

    Vector3 input, moveDirection;
    CharacterController _controller;
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
        input *= moveSpeed;

        // transform.Translate(input);

        if (_controller.isGrounded)
        {
            // we can jump here
            moveDirection = input;
            //  Debug.Log("ground");
            if (Input.GetButton("Jump"))
            {
                // jump the shark

                //  Debug.Log("please jump");
                anim.SetInteger("animState", 2);
                moveDirection.y = Mathf.Sqrt(2 * gravity * jumpAmount);
                updateSpread(3);

            }
            else
            {
                // ground the object
                moveDirection.y = 0.0f;
            }
        }
        else
        {
            // midair          
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, Time.deltaTime * airControl);
            //  Debug.Log("air");
            updateSpread(3);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            jumpAmount = 2;
            _controller.Move(moveDirection * Time.deltaTime);
            anim.SetInteger("animState", 1);
            updateSpread(2);
        }
        else
        {
            jumpAmount = 1;
            _controller.Move(moveDirection * 2.75f * Time.deltaTime);
            anim.SetInteger("animState", 4);
            updateSpread(3);

        }

        if (moveHorizontal != 0 || moveVertical != 0)
        {

        }
        else
        {
            anim.SetInteger("animState", 0);
            updateSpread(1);
        }

        if (Input.GetButton("Fire1"))
        {
            anim.SetInteger("animState", 3);
        }

        if (input.magnitude > 0.01f)
        {
            float cameraYawRotation = Camera.main.transform.eulerAngles.y;
            Quaternion newRotation = Quaternion.Euler(0, cameraYawRotation, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 10);
        }

    }

    void updateSpread(int modifier)
    {
        // var gunSpread = GameObject.FindGameObjectWithTag("Gun").GetComponent<PlayerShooter>();
        // gunSpread.spreadFactor = modifier;
    }
}
