using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float gravity = 9.81f;
    public float jumpHeight = 1.0f;
    public float airControl = 1.0f;

    Vector3 input, moveDirection;
    CharacterController _controller;

    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
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

            if (Input.GetButton("Jump"))
            {
                // jump the shark
                moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
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
        }

        moveDirection.y -= gravity * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _controller.Move(moveDirection * 1.5f * Time.deltaTime);
        }
        else { 
        _controller.Move(moveDirection * Time.deltaTime);
        }
    }
}
