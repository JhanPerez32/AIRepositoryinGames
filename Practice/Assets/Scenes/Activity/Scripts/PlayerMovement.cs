using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum OffMeshMoveMethod
{
    Parabola,
    Curve
}

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Camera Camera = null;
    private NavMeshAgent Agent;

    private RaycastHit[] Hits = new RaycastHit[1];

    public OffMeshMoveMethod moveMethod;
    public AnimationCurve animationCurve = new();

    public float jumpHeight;
    public float duration;

    private IEnumerator Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        while (true)
        {
            if (Agent.isOnOffMeshLink)
            {
                if (moveMethod == OffMeshMoveMethod.Parabola)
                {
                    yield return StartCoroutine(Parabola(Agent, jumpHeight, duration));
                }
                else if (moveMethod == OffMeshMoveMethod.Curve)
                {
                    yield return StartCoroutine(Curve(Agent, 0.5f));
                }
                Agent.CompleteOffMeshLink();
            }
            yield return null;
        }
    }

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.RaycastNonAlloc(ray, Hits) > 0)
            {
                Agent.SetDestination(Hits[0].point);
            }
        }
    }

    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;

        float _normalizeTime = 0.0f;
        while (_normalizeTime < 1.0f)
        {
            float _yOffset = height * 4.0f * (_normalizeTime - _normalizeTime * _normalizeTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, _normalizeTime) + _yOffset * Vector3.up;
            _normalizeTime += Time.deltaTime / duration;
            yield return null;
        }
    }

    IEnumerator Curve(NavMeshAgent agent, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;

        float _normalizeTime = 0.0f;
        while (_normalizeTime < 1.0f)
        {
            float _yOffset = animationCurve.Evaluate(_normalizeTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, _normalizeTime) + _yOffset * Vector3.up;
            _normalizeTime += Time.deltaTime / duration;
            yield return null;
        }
    }
}
