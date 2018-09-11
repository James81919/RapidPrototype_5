using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    [Header("Stats")]
    public float attackDamage;
    public float attackSpeed;
    public float attackRange = 3f;
    public float attackRadius = 1f;
    public float maxHealth = 100;
    public float currHealth;

    [Header("Movement")]
    public bool isWandering;
    public float wanderOffsetX;
    public float wanderOffsetZ;
    public float ChangeDirectionSpeed;

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

        // Shoot out a ray infront of the player
        Ray attackRay = new Ray(this.transform.position, this.transform.forward);

        RaycastHit[] raycastHits;
        // Cast out the ray as a sphere shape in the attack range
        raycastHits = Physics.SphereCastAll(attackRay, attackRadius, attackRange, LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);
        Debug.DrawRay(transform.position, transform.forward * attackRange, Color.blue, 1f, false);

        //anim.SetTrigger("Attack");
        foreach (RaycastHit hitResult in raycastHits)
        {
            Debug.Log("Hit: " + hitResult.transform.gameObject.name);
            // Do whatever the other object needs to be react
            //hitResult.transform.GetComponent<>

            // If is PLAYER
            if (hitResult.transform.gameObject.tag == "Player")
            {
                hitResult.transform.GetComponent<Player>().TakeDamage(attackDamage);
            }
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Attack(collision.gameObject));
        }
    }

    IEnumerator MoveToPos()
    {
        Vector3 newPos = new Vector3(Random.Range(transform.position.x - wanderOffsetX, transform.position.x + wanderOffsetX),
            transform.position.y,
            Random.Range(transform.position.z - wanderOffsetZ, transform.position.z + wanderOffsetZ));

        agent.SetDestination(newPos);
        yield return new WaitForSeconds(ChangeDirectionSpeed);

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

    IEnumerator Attack(GameObject player)
    {
        yield return new WaitForSeconds(attackSpeed);
        player.GetComponent<Player>().TakeDamage(attackDamage);
    }
}
