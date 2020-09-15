using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public HealthBar mHealthBar;
    void Start()
    {
        currentHealth = maxHealth;
        mHealthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    TakeDamage(20.0f);
        //}
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        mHealthBar.setHealth (currentHealth);
    }
}
