using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IKillable
{
    [Header("Stats")]
    public float TotalHealth = 1f;
    public float CurrHealth;

	void Start ()
    {
        // Set to full health at the beginning
        CurrHealth = TotalHealth;
    }

    /* Interface Implementation =================================*/
    
    // IKillable
    public void TakeDamage(float _value)
    {
        CurrHealth -= _value;
        CheckDeath();
    }
    public void CheckDeath()
    {
        if (IsAlive() == false)
        {
            KillEntity();
        }
    }
    public void KillEntity()
    {
        Debug.Log("Rock Dead");

        /// Play Rock death animation

        /// Spawn the blue soul crystal
        
        /// De-spawn the object
    }
    public bool IsAlive()
    {
        if (CurrHealth <= 0f)
        {
            return false;
        }
        return true;
    }


    // ============================================================
}
