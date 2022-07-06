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
    void Update()
    {
        
    }
}
