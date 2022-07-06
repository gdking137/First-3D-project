using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private KeyCode jumpKeyCode = KeyCode.Space ;
    [SerializeField]
    private CameraController cameraController;
    private Movement3D movement3D;
    
    
    
    
    private void Awake()
    {
        movement3D = GetComponent<Movement3D>();
    }




    void Update()
    {

        //x and y movement of the player
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");   

        movement3D.MoveTo(new Vector3(x, 0 , z));



        //jump to effect the y direction
        if(Input.GetKeyDown(jumpKeyCode))
        {
            movement3D.JumpTo();
        }

        float mouseX = Input.GetAxis("Mouse X");     // mouse left/right movement
        float mouseY= Input.GetAxis("Mouse Y");      // mouse up/down movement
        cameraController.RotateTo(mouseX, mouseY);
    }
}
