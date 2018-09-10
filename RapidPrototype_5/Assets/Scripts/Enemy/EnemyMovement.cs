using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public bool isWandering;
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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isWandering = false;
            agent.SetDestination(other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isWandering = true;
            StartCoroutine(MoveToPos());
        }
    }

    IEnumerator MoveToPos()
    {
        Vector3 newPos = new Vector3(Random.Range(minXWanderDistance, maxXWanderDistance),
            transform.position.y, Random.Range(minZWanderDistance,
            maxZWanderDistance));

        agent.SetDestination(newPos);
        yield return new WaitForSeconds(3);

        if (isWandering)
        {
            StartCoroutine(MoveToPos());
        }
    }
}
