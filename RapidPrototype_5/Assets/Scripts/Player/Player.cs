using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	[Header("Player Stat")]
	public float TotalHealth;
	public float CurrHealth;


	private GameObject m_healthBarUI;

	void Awake()
	{
		m_healthBarUI = 
			transform.Find("PlayerUICanvas/PlayerHealthBar").gameObject;
	}

	// Use this for initialization
	void Start()
	{
		CurrHealth = TotalHealth;
	}
	
	// Update is called once per frame
	void Update()
	{
		// Set the health bar to current health by percentage
		m_healthBarUI.GetComponent<Slider>().value = 
			CurrHealth / TotalHealth;
	}
}
