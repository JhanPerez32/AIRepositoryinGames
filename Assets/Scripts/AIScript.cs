using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIScript : MonoBehaviour
{
    private NavMeshAgent agent;
    public ObstacleAvoidanceType AvoidanceType;
    public float AvoidancePredictionTime;
    public float AgentSpeed;
    public Transform Destination;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        NavMesh.avoidancePredictionTime = AvoidancePredictionTime;
        agent.obstacleAvoidanceType = AvoidanceType;
        agent.speed = AgentSpeed;
        agent.SetDestination(Destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
