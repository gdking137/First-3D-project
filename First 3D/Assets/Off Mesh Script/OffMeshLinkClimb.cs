using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class OffMeshLinkClimb : MonoBehaviour
{

    [SerializeField]
    private int             offMeshArea = 3;
    [SerializeField]
    private float           climbSpeed = 1.5f;
    private NavMeshAgent    navMeshAgent;

    // Start is called before the first frame update
    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    IEnumerator Start()
    {
        while(true)
        {
            //repeat until IsOnClimb() outcome is true
            yield return new WaitUntil(() => IsOnClimb());

            //act of climbing up and down
            yield return StartCoroutine(ClimbOrDescend());
        }
    }

    public bool IsOnClimb()
    {
        //is the object at a place where OffMeshLink is (true/false)
        if (navMeshAgent.isOnOffMeshLink)
        {
            //this location's OffMeshLink data
            OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;

            //if NavMeshAgent.currentOffMeshLinkData.offMeshLink is
            //TRUE = manually created offMeshLink
            //FALSE= Automatically created offMeshLink


            //if current location's OFFMeshLink= manually created + location's DATA = "Climb" then...
            if (linkData.offMeshLink != null && linkData.offMeshLink.area == offMeshArea)
            {
                return true;
            }
        }
        return false;
    }


    // Update is called once per frame
private IEnumerator ClimbOrDescend()
    {

        //stop moving with navMeshAgent
        navMeshAgent.isStopped = true;


        //current position's offMeshLink's start/end point
        OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;
        Vector3 start = linkData.startPos;
        Vector3 end = linkData.endPos;


        //time for climbing up/down
        float climbTime = Mathf.Abs(end.y - start.y) / climbSpeed;
        float currentTime = 0.0f;
        float percent = 0.0f;



        while (percent <1)
            {
            //Time.deltaTime will make PERCENT= 1 after 1 SECOND
            // so use climbTime variable to control time
            currentTime += Time.deltaTime;
            percent = currentTime / climbTime;


            //after time(max 1 second) change object's position
            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        //finished moving using OFFMESHLINK
        navMeshAgent.CompleteOffMeshLink();

        //After OFFMESHLINK is done, reuse navigation to move. 
        navMeshAgent.isStopped = false;

    }
}
