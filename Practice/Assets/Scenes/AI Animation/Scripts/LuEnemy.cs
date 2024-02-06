using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*public enum e_AI_State
{
    FollowPlayer,
    Patrol
}*/

[RequireComponent(typeof(NavMeshAgent))]
public class LuEnemy : MonoBehaviour
{
    //public e_AI_State aiState;
    public Transform playerPosition;
    public NavMeshAgent agent;
    public Transform Waypoints;
    public List<Transform> targetWaypoint;
    public int wayPointNumber;
    public bool isMoving;
    public float playerFollowRange;
    public float followDuration;
    public float chaseSpeed;

    public float distanceToPlayer;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerPosition = GameObject.FindWithTag("Player").transform;
        foreach (Transform tr in Waypoints.GetComponentsInChildren<Transform>())
        {
            targetWaypoint.Add(tr.gameObject.transform);
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition.position);
        
    }

    public void MoveToRandomWaypoint()
    {
        if (targetWaypoint.Count == 0)
        {
            Debug.LogWarning("No waypoints available.");
            return;
        }
        int newWaypointIndex = GetRandomWaypointIndex();

        if (newWaypointIndex != wayPointNumber)
        {

            wayPointNumber = newWaypointIndex;

            agent.SetDestination(targetWaypoint[wayPointNumber].position);
        }
        else
        {
            MoveToRandomWaypoint();
        }
    }

    private int GetRandomWaypointIndex()
    {
        return Random.Range(0, targetWaypoint.Count);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerFollowRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceToPlayer);
    }
}
