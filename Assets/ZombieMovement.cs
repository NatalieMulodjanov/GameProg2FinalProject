using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool releaseZombie = false;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if(!isGrounded) {
            //Debug.Log("FALL");
        }

        if(isGrounded){
            //Debug.Log("On Platform");
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void FixedUpdate() {
        if(releaseZombie == true){
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
