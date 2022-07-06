using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    private NewMoveMent3D NewMovement;


    private void Awake()
    {
        NewMovement = GetComponent<NewMoveMent3D>();
    }

    void Update()
    {
        if( Input.GetMouseButtonDown(0)) //when clicked with a left button
        {
            RaycastHit hit;
            // Camera.main = object that has the "Camera tag", which is the "Main Camera
            //from the camera start a ray that passes by the mouse point (Input.mousePosition)
            //ray.origin : start of the ray 
            //ray.direction :direction of the ray


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        


            //physics.Raycast() : shoots the ray and figure out what it hits
            //(if ray hits anything, then return "true")
            //from the origin, shoot an infinitly long ray towards the ray.direction
            //save objects detected by the ray to the "hit"

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // hit.transform.position : location of the object that ray hit
                // hit.point : exact position where the ray hit the object


                //set hit.point as our destination!
                NewMovement.MoveTo(hit.point);
            }
        }

    }
}
