using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float rotateSpeedX = 3;
    private float rotateSpeedY = 3;
    private float limitMinX = -80;
    private float limitMaxX = 50;
    private float eulerAngleX;
    private float eulerAngleY;

    public void RotateTo(float mouseX, float mouseY)
    {
        // reason why we integrate y-axis into mouseX value that controls mouse left/right
        // when moving a move left/right, camera object also has to spin from a y-axis
        eulerAngleY += mouseX * rotateSpeedX;


        //ditto for looking down/up, touch the X axis not the Y
        eulerAngleX -= mouseY * rotateSpeedY;

        //limit the angle
        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        //apply object to the quaternion rotation
        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);


    }


    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;


        // set this so the angle ddoesn't go over the set number
        return Mathf.Clamp(angle, min, max);
    }
}
