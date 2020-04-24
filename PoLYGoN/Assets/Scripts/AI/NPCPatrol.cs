using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    [SerializeField]
    bool patrolWaiting = false;             //decides wether npc waits

    [SerializeField]
    float totalWaitTime = 3f;       //wait time at each node

    [SerializeField]
    float switchProbability = 0.2f; //probability of switching direction

    [SerializeField]
    List<Waypoint> patrolPoints = null;    //list of all nodes to visit

    //private variables for base behavior
    NavMeshAgent agent;
    int currentPatrolIndex;
    bool travelling;
    bool waiting;
    bool patrolForward;
    float waitTimer;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if(agent == null)
        {
            Debug.Log("nav mesh agent not attached to "+gameObject.name);
        }
        else
        {
            if(patrolPoints != null && patrolPoints.Count >=2)
            {
                currentPatrolIndex =0;
                SetDestination();
            }
            else
            {
                Debug.Log("insufficient patrol points");
            }
        }
    }

    public void Update()
    {
        if(travelling && agent.remainingDistance <=1.0f)
        {
            travelling = false;

            //if waiting
            if(patrolWaiting)
            {
                waiting = true;
                waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        if(waiting)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer >= totalWaitTime)
            {
                waiting = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if(patrolPoints != null)
        {
            Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
            agent.SetDestination(targetVector);
            travelling = true;
        }
    }

    private void ChangePatrolPoint()
    {
        if(UnityEngine.Random.Range(0f,1f) <= switchProbability)
        {
            patrolForward = !patrolForward;
        }

        if(patrolForward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        else
        {
            if(--currentPatrolIndex <0)
            {
                currentPatrolIndex = patrolPoints.Count -1;
            }
        }
    }

}
