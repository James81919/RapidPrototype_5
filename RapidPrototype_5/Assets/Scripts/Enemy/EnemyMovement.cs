using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour, IKillable
{

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

    // Navigation Agent
    private NavMeshAgent agent;

    public SkinnedMeshRenderer meshren;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        StartCoroutine(MoveToPos());
        currHealth = maxHealth;
    }
    private void Update()
    {
        // Shoot out a ray infront of the player
        Ray attackRay = new Ray(this.transform.position, this.transform.forward);

        RaycastHit[] raycastHits;
        // Cast out the ray as a sphere shape in the attack range
        raycastHits = Physics.SphereCastAll(attackRay, attackRadius, attackRange, LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);
        Debug.DrawRay(transform.position, transform.forward * attackRange, Color.blue, 1f, false);

        //anim.SetTrigger("Attack");
        //foreach (RaycastHit hitResult in raycastHits)
        //{
        //    hitResult.transform.GetComponent<IKillable>().
        //        TakeDamage(attackDamage);
        //}
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isWandering = false;
            agent.SetDestination(other.transform.position);
            agent.updateRotation = true;
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
        agent.updateRotation = true;
        yield return new WaitForSeconds(ChangeDirectionSpeed);

        if (isWandering)
        {
            StartCoroutine(MoveToPos());
        }
    }
    IEnumerator DamageEffect()
    {
        meshren.material.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(0.1f);
        meshren.material.color = new Color(1, 1, 1);
    }
    IEnumerator Attack(GameObject player)
    {
        yield return new WaitForSeconds(attackSpeed);
        player.GetComponent<Player>().TakeDamage(attackDamage);
    }

    /* Interface Implementation =================================*/

    // IKillable
    public void TakeDamage(float _value)
    {
        currHealth -= _value;
        StartCoroutine(DamageEffect());
        CheckDeath();
    }
    public void CheckDeath()
    {
        if(IsAlive() == false)
        {
            KillEntity();
        }
    }
    public void KillEntity()
    {
        Destroy(gameObject);
    }
    public bool IsAlive()
    {
        if (currHealth <= 0)
        {
            return false;
        }
        return true;
    }

    // ============================================================
}
