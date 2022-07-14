using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePatrol : MonoBehaviour
{
    [SerializeField]

    private Transform[]      paths;
    private int              currentPath = 0;
    private float            moveSpeed = 3.0f;

    private void Update ()
    {
        // settting movement direction : (End destination - current position). normalized(?)
        Vector3 direction = (paths[currentPath].position - transform.position).normalized;

        //object movement
        transform.position += direction * moveSpeed * Time.deltaTime;

        //when arrived at the end destinaction
        if((paths[currentPath].position - transform.position).sqrMagnitude < 0.1f)
        {
            //changing end destination (patrol route repeated)
            if( currentPath < paths.Length - 1 ) currentPath++;
            else                                 currentPath--;     
        }
    }
    
 
}
