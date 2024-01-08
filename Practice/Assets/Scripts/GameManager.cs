using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public struct AiVariants
    {
        public TextMeshProUGUI textMeshPro;
        public Transform targetAI;
        public Vector3 offset;
     
    }

    public AiVariants[] aiVariants;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var pair in aiVariants)
        {
            if(pair.textMeshPro != null && pair.targetAI != null)
            {
                Vector3 targetPos = pair.targetAI.position + pair.offset;
                pair.textMeshPro.transform.position = Camera.main.WorldToScreenPoint(targetPos);
                float targetAvoidanceQuality = pair.targetAI.GetComponent<NavMeshAgent>().avoidancePriority;
                pair.textMeshPro.text = targetAvoidanceQuality.ToString();

            }
        }
    }
}
