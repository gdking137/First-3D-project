using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshLinkJump : MonoBehaviour
{
    [SerializeField]
    private float jumpSpeed = 10.0f;
    [SerializeField]
    private float gravity = -9.81f;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();    
    }

    IEnumerator Start()
    {
        while(true)
        {
            //repeatedly call this function until IsOnJump() function is true
            yield return new WaitUntil(() => IsOnJump());


            //jump action
            yield return StartCoroutine(JumpTo());
        }
    }

    public bool IsOnJump()
    {
        if (navMeshAgent.isOnOffMeshLink)
        {
            //current position's OffMeshLink data
            OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;

            ///****OffMeshLinkType ==   Manual = 0, DropDown = 1, JumpAcross = 2
            ///Use this (1,2) to distinct the randomized OffMeshLink

            //if current position's OFFMESHLINK's OFFMESHLINKTYPE == JumpAcrross
            if (linkData.linkType == OffMeshLinkType.LinkTypeJumpAcross ||
                    linkData.linkType == OffMeshLinkType.LinkTypeDropDown)
            {
                return true;
            }
        }
        return false;
    }


    IEnumerator JumpTo()
    {
        //Stop moving with Navigation
        navMeshAgent.isStopped = true;

        // location of the Start/End location of OffMeshLink
        OffMeshLinkData linkData= navMeshAgent.currentOffMeshLinkData;
        Vector3 start = transform.position;
        Vector3 end = linkData.endPos;

        //time set for jumping
        float jumpTime      = Mathf.Max(0.3f, Vector3.Distance(start, end)/ jumpSpeed);
        float currentTime   = 0.0f;
        float percent       = 0.0f;

        float v0 = (end - start).y - gravity;  //y direction's initial speed

        while (percent < 1)
        {
            //Time.deltaTime will make PERCENT= 1 after 1 SECOND
            // so use climbTime variable to control time
            currentTime += Time.deltaTime;
            percent = currentTime/ jumpTime;


            //after time(max 1 second) change object's (x,z) position
            Vector3 position = Vector3.Lerp(start, end, percent);

            
            //change the object's Y position based on time
            //arch = initial location + (initial time * time) + (gravity * percent squared)
            position.y= start.y + (v0 * percent) + (gravity * percent * percent);
            

            //integrate the measured x,y,z value to the actual OBJECT
            transform.position = position;

            yield return null;

        }

        //finished moving using OFFMESHLINK
        navMeshAgent.CompleteOffMeshLink();

        //After OFFMESHLINK is done, reuse navigation to move. 
        navMeshAgent.isStopped = false;

    }



}















