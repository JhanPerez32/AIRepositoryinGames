using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIRandomNavi : MonoBehaviour
{
    public Transform WayPoint;
    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(agent == null || WayPoint == null)
        {
            Debug.Log("No Agent or Waypoint");
            enabled = false;
            return;
        }
        SetDestination(WayPoint.transform.position);
    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            Debug.Log("Path Ended");
        }
    }
}
