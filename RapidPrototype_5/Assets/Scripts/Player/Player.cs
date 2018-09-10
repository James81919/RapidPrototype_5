using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
	[Header("Player Stat")]
	public float TotalHealth = 100f;
	public float CurrHealth;
	public float AttackDmg = 50f;
	public float Deffence = 50f;
	public float Speed = 300f;

	// Player HUD ===================
	private GameObject m_healthBarUI;
	// ==============================

	// Player stats panel ===========
	private GameObject m_statsCanvas;
	private TextMeshProUGUI atkLabel;
	private TextMeshProUGUI defLabel;
	private TextMeshProUGUI spdLabel;
	// ==============================

	void Awake()
	{
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

	// Use this for initialization
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
}
