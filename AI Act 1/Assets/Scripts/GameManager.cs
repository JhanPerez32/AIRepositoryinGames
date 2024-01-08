using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private Dictionary<Transform, Vector3> initialPositions = new Dictionary<Transform, Vector3>();

    public string targetTag = "";
    public TextMeshProUGUI TimerText;
    public float Timer = 0.0f;
    public bool StopTimer;

    //Destination Checker
    public List<Transform> destinations; // List of destination points (you can assign these in the Inspector)
    private List<NavMeshAgent> agents = new List<NavMeshAgent>(); // List to hold all NavMeshAgents

    void Start()
    {
        StopTimer = false;
        Timer = 0.0f;
        NavMeshAgent[] navAgents = FindObjectsOfType<NavMeshAgent>();
        // Add all found NavMeshAgents to the agents list
        foreach (NavMeshAgent agent in navAgents)
        {
            agents.Add(agent);
        }
        SaveInitialPositions();
    }

    void SaveInitialPositions()
    {
        GameObject[] objectsToReset = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject obj in objectsToReset)
        {
            initialPositions[obj.transform] = obj.transform.position;
        }
    }

    public void ResetObjectsToInitialPositions()
    {
        Timer = 0.0f;
        foreach (Transform objTransform in initialPositions.Keys)
        {
            objTransform.position = initialPositions[objTransform];
        }
    }

    [System.Serializable]
    public struct AiVariants_Blue
    {
        public TextMeshProUGUI textMeshPro;
        public Transform targetObject;
        public Vector3 offset;
    }

    [System.Serializable]
    public struct AiVariants_Red
    {
        public TextMeshProUGUI textMeshPro;
        public Transform targetObject;
        public Vector3 offset;
    }

    [System.Serializable]
    public struct AiVariants_White
    {
        public TextMeshProUGUI textMeshPro;
        public Transform targetObject;
        public Vector3 offset;
    }

    [System.Serializable]
    public struct AiVariants_Green
    {
        public TextMeshProUGUI textMeshPro;
        public Transform targetObject;
        public Vector3 offset;
    }

    public AiVariants_Blue[] blueVariants;
    public AiVariants_Red[] redVariant;
    public AiVariants_White[] whiteVariants;
    public AiVariants_Green[] greenVariant;

    void Update()
    {
        if (!StopTimer) { Timer += Time.deltaTime; }
        else { StopTimer = true; }

        TimerText.text = Timer.ToString();
        foreach (var pair in blueVariants)
        {
            if (pair.textMeshPro != null && pair.targetObject != null)
            {
                Vector3 targetPosition = pair.targetObject.position + pair.offset;
                pair.textMeshPro.transform.position = Camera.main.WorldToScreenPoint(targetPosition);
                float targetAvoidanceQuality = pair.targetObject.GetComponent<NavMeshAgent>().avoidancePriority;
                pair.textMeshPro.text = targetAvoidanceQuality.ToString();

            }
        }

        foreach (var pair in redVariant)
        {
            if (pair.textMeshPro != null && pair.targetObject != null)
            {
                Vector3 targetPosition = pair.targetObject.position + pair.offset;
                pair.textMeshPro.transform.position = Camera.main.WorldToScreenPoint(targetPosition);
                float targetAvoidanceQuality = pair.targetObject.GetComponent<NavMeshAgent>().avoidancePriority;
                pair.textMeshPro.text = targetAvoidanceQuality.ToString();

            }
        }

        foreach (var pair in whiteVariants)
        {
            if (pair.textMeshPro != null && pair.targetObject != null)
            {
                Vector3 targetPosition = pair.targetObject.position + pair.offset;
                pair.textMeshPro.transform.position = Camera.main.WorldToScreenPoint(targetPosition);
                float targetAvoidanceQuality = pair.targetObject.GetComponent<NavMeshAgent>().avoidancePriority;
                pair.textMeshPro.text = targetAvoidanceQuality.ToString();

            }
        }

        foreach (var pair in greenVariant)
        {
            if (pair.textMeshPro != null && pair.targetObject != null)
            {
                Vector3 targetPosition = pair.targetObject.position + pair.offset;
                pair.textMeshPro.transform.position = Camera.main.WorldToScreenPoint(targetPosition);
                float targetAvoidanceQuality = pair.targetObject.GetComponent<NavMeshAgent>().avoidancePriority;
                pair.textMeshPro.text = targetAvoidanceQuality.ToString();

            }
        }
        bool allReached = true;

        foreach (NavMeshAgent agent in agents)
        {
            // Check if the agent is actively pathfinding
            if (agent.pathPending)
                continue;

            // If the agent still has a path to follow, it hasn't reached the destination
            if (!agent.hasPath || agent.remainingDistance > agent.stoppingDistance)
            {
                allReached = false;
                break;
            }
        }

        if (allReached)
        {
            Debug.Log("All agents have reached their destinations!");
            StopTimer = true;
        }
    }
}
