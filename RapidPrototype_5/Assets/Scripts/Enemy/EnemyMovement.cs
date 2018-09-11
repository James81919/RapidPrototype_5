using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    [Header("Stats")]
    public float attackDamage;
    public float attackSpeed;
    public float maxHealth;
    public float currHealth;

    [Header("Movement")]
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
        currHealth = maxHealth;
    }

    private void Update()
    {
        if (currHealth <= 0)
        {
            Destroy(gameObject);
        }
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

    public void TakeDamage(float damage)
    {
        currHealth -= damage;
        StartCoroutine(DamageEffect());
    }

    IEnumerator DamageEffect()
    {
        GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(0.1f);
        GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
    }
}
