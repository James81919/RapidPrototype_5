using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public float minXWanderDistance;
    public float maxXWanderDistance;
    public float minZWanderDistance;
    public float maxZWanderDistance;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(MoveToPos());
    }

    private void Update()
    {
        
    }

    IEnumerator MoveToPos()
    {
        Vector3 newPos = new Vector3(Random.Range(minXWanderDistance, maxXWanderDistance),
            transform.position.y, Random.Range(minZWanderDistance,
            maxZWanderDistance));

        agent.SetDestination(newPos);
        yield return new WaitForSeconds(3);
        StartCoroutine(MoveToPos());
    }
}
