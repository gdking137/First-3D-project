using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField]
    private Transform       target;                 //object that camera chases
    [SerializeField]
    private float           minDistance = 3;        //minimum distance between the camera and the target
    [SerializeField]
    private float           maxDistance = 30;       //maximum distance between the camera and the target
    [SerializeField]
    private float           wheelSpeed = 500;       //mouse wheel scroll speed
    [SerializeField]
    private float           xMoveSpeed = 500;       //camera's y axis rotational speed
    [SerializeField]
    private float           yMoveSpeed = 250;       //camera's x axis rotational speed
    private float           yMinLimit = 5;          //camera's x axis rotation minimum limit 
    private float           yMaxLimit = 80;         //camera's x axis rotation maximum limit
    private float           x, y;                   //mouse movement direction value
    private float           distance;               //distance between the camera and the target


    private void Awake()
    {
        //reset the value "distance" based upon the position of the CAMERA and the TARGET
        distance = Vector3.Distance(transform.position, target.position);
        
        //save the initial camera's rotation value to X and Y
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }


    private void Update()
    {
        if (target == null) return;   //if the target doesn't exist, then this won't execute

        if (Input.GetMouseButtonDown(1))    //is the player right click
        {
            //mouse's x, y axis directional data
            x += Input.GetAxis("Mouse X") * xMoveSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * yMoveSpeed * Time.deltaTime;

            y = ClampAngle(y, yMinLimit, yMaxLimit);    //object's up/down limit setting

            transform.rotation = Quaternion.Euler(y, x, 0); //camera's rotational data 
        }


        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;  //use mouse's wheel to control the distance between CAMERA and TARGET
        distance = Mathf.Clamp(distance, minDistance, maxDistance);                    //set the MAX and MIN value so it doesn't get out of hand


    }

    private void LateUpdate()
    {
        if( target == null) return;         //if the target doesn't exist then this won't execute 

        // Set the Camera's position data
        // based on the TARGET's position, Distance the camera based on the location of the TARGET
        transform.position = transform.rotation * new Vector3(0, 0, -distance) + target.position; 
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);    
    }
}
