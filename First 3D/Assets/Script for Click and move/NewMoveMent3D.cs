using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewMoveMent3D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    private NavMeshAgent navMeshAgent;



    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 goalPosition)
    {

        StopCoroutine("OnMove"); // stop the pre-existing coroutine

        navMeshAgent.speed = moveSpeed; //speed setting

        navMeshAgent.SetDestination(goalPosition); // set the destination

        StartCoroutine("OnMove"); // start the coroutine about the "OnMove"
    }

    IEnumerator OnMove()
    {
        while (true)
        {
            // if my destination (navMeshAgent.destination) and my position (transform.position) distance is less than 0.1
            //AKA if i get close to my destination 
            if (Vector3.Distance(navMeshAgent.destination, transform.position) <0.1f)
            {
                //set my position as the aimed destination
                transform.position = navMeshAgent.destination;

                navMeshAgent.ResetPath();  //reset the route setting by SetDestination and stop moving

                break;
            }
            yield return null;
        }
    }


}

  
