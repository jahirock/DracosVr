using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionReference move = null;

    public float speed = 5F;

    Rigidbody rb;

    Vector3 moveDirection;

    CharacterController controller;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector2 dir = move.action.ReadValue<Vector2>();
        Debug.Log(dir);
        //GetComponent<Rigidbody>().velocity = new Vector3(dir.x, 0, dir.y) * speed;
        
        if(dir != Vector2.zero)
        {
            moveDirection = (transform.right * dir.x) + (transform.forward * dir.y);
        }

        controller.Move(moveDirection * speed * Time.deltaTime);
        controller.Move(Physics.gravity/5);
        //rb.velocity = ((moveDirection + Physics.gravity)*speed);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("move! x: " + context.ReadValue<Vector2>().x + ", y: " + context.ReadValue<Vector2>().y);

        moveDirection = (transform.right * context.ReadValue<Vector2>().x) + (transform.forward * context.ReadValue<Vector2>().y);
        //moveDirection = new Vector3(context.ReadValue<Vector2>().x, Physics.gravity.y, context.ReadValue<Vector2>().y);
    }
}
