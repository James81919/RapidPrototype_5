using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour, IKillable, ICombatEntity
{
	[Header("Player Stat")]
	public float TotalHealth = 100f;
	public float CurrHealth;
	public float AttackDmg = 50f;
	public float Deffence = 50f;
	public float Speed = 300f;
    public float AttackRange = 3f;
    public float AttackRadius = 1f;

    [Header("Config")]
    public LayerMask AttackingLayer;


    // Player HUD ===================
    private GameObject m_healthBarUI;
	// ==============================

	// Player stats panel ===========
	private GameObject m_statsCanvas;
	private TextMeshProUGUI atkLabel;
	private TextMeshProUGUI defLabel;
	private TextMeshProUGUI spdLabel;
    // ==============================

    private Animator anim;

	void Awake()
	{
        anim = GetComponentInChildren<Animator>();

		// Reference the health UI
		m_healthBarUI = 
			transform.Find("PlayerUICanvas/PlayerHealthBar").gameObject;

		// Reference the stats panel UI
		m_statsCanvas = 
			transform.Find("PlayerUICanvas/StatsPanelCanvas").gameObject;
		atkLabel = m_statsCanvas.transform.
			Find("AttackValue").gameObject.GetComponent<TextMeshProUGUI>();
		defLabel = m_statsCanvas.transform.
			Find("AttackValue").gameObject.GetComponent<TextMeshProUGUI>();
		spdLabel = m_statsCanvas.transform.
			Find("AttackValue").gameObject.GetComponent<TextMeshProUGUI>();
	}
	void Start()
	{
        // Set the health to full health
        CurrHealth = TotalHealth;

		// Hide the stats panel at the beginning
		m_statsCanvas.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		UpdateHealthBar();


		if (Input.GetKeyDown(KeyCode.P))
		{
			StatsPanelOnOff();
		}

        if (Input.GetButtonDown("Attack"))
        {
            StartCoroutine(LightAttack());
        }
	}

    private IEnumerator LightAttack()
    {
        // Shoot out a ray infront of the player
        Ray attackRay = new Ray(this.transform.position, this.transform.forward);

        RaycastHit[] raycastHits;
        // Cast out the ray as a sphere shape in the attack range
        raycastHits = Physics.SphereCastAll(attackRay, AttackRadius, AttackRange, AttackingLayer, QueryTriggerInteraction.Ignore);
        Debug.DrawRay(transform.position, transform.forward * AttackRange, Color.blue, 2f, false);

        anim.SetBool("IsAttacking", true);
        foreach (RaycastHit hitResult in raycastHits)
        {
            Debug.Log("Hit: " + hitResult.transform.gameObject.name);
            // Do whatever the other object needs to be react
            //hitResult.transform.GetComponent<>

            // If is ENEMY
            if (hitResult.transform.gameObject.tag == "Enemy")
            {
                hitResult.transform.GetComponent<EnemyMovement>().TakeDamage(AttackDmg);
            }
        }
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("IsAttacking", false);
    }
	private void UpdateHealthBar()
	{
		// Set the health bar to current health by percentage
		m_healthBarUI.GetComponent<Slider>().value =
			CurrHealth / TotalHealth;
	}
	private void StatsPanelOnOff()
	{
		if (m_statsCanvas.activeSelf == false)
		{
			m_statsCanvas.SetActive(true);
			UpdateStatsPanel();
		}
		else if (m_statsCanvas.activeSelf == true)
		{
			m_statsCanvas.SetActive(false);
		}
	}
	private void UpdateStatsPanel()
	{
		atkLabel.SetText(AttackDmg.ToString());
		defLabel.SetText(Deffence.ToString());
		spdLabel.SetText(Speed.ToString());
	}

    /* Interface Implementation =================================*/

    // IDamageable
    public void TakeDamage(float _value)
    {
        CurrHealth -= _value;

    }
    public void CheckDeath()
    {
        if (CurrHealth <= 0f)
        {
            KillEntity();
        }
    }
    public void KillEntity()
    {
        anim.SetBool("IsDead", true);
        /// Do some code when the player dies
    }
    public bool IsAlive()
    {
        if (CurrHealth <= 0f)
        {
            return false;
        }
        return true;
    }

    // ICombatEntity




    // ============================================================


}
