using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AICapsule : MonoBehaviour
{
    private NavMeshAgent agent;
    public ObstacleAvoidanceType AvoidanceType;
    public float AvoidancePredictionTime;
    public float AgentSpeed;
    public Transform Destination;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        NavMesh.avoidancePredictionTime = AvoidancePredictionTime;
        agent.obstacleAvoidanceType = AvoidanceType;
        agent.SetDestination(Destination.position);
    }

    private void Update()
    {
        
    }
}
