using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMultiWayP : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform waypoint;
    public List<Transform> waypoints;
    public bool isMoving;
    public int wayPointNumber;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        foreach (Transform transform in waypoint.GetComponentInChildren<Transform>())
        {
            waypoints.Add(transform.gameObject.transform);
        }
        MoveToRandomPoints();
    }

    private void MoveToRandomPoints()
    {
        if (waypoints.Count < 0)
        {
            Debug.Log("No Waypoints Detected");
            return;
        }
        isMoving = true;
        int newWayPointsIndex = GetRandomWayPoints();

        if (newWayPointsIndex != wayPointNumber)
        {
            wayPointNumber = newWayPointsIndex;
            agent.SetDestination(waypoints[wayPointNumber].position);
        }
    }

    private int GetRandomWayPoints()
    {
        return Random.Range(0, waypoints.Count);
    }

    void Update()
    {
        if (!agent.pathPending)
        {
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                if(!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    MoveToRandomPoints();
                    Debug.Log("Path Ended and wil random");
                }
            }
        }
    }
}
