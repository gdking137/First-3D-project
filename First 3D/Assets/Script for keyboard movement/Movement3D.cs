using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [SerializeField]

    private float       moveSpeed = 5.0f;    //speed
    private float       gravity = -9.81f;    // gravity
    private Vector3     moveDirection;       // movement direction
    private float       jumpForce = 3.0f;    // jumping power

    [SerializeField]
    private Transform           cameraTransform;        //camera transform component
    private CharacterController characterController;



    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move(moveDirection* moveSpeed *Time.deltaTime);

        if (characterController.isGrounded == false)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
    }

    public void MoveTo(Vector3 direction)
    {
        //moveDirection = new Vector3(direction.x, moveDirection.y, direction.z);
        Vector3 movedis = cameraTransform.rotation * direction;
        moveDirection = new Vector3(movedis.x, moveDirection.y, movedis.z);
    }

    public void JumpTo()
    {
        if(characterController.isGrounded == true)
        {
            moveDirection.y = jumpForce;
        }
    }
}
