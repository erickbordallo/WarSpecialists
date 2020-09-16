using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minion : MonoBehaviour
{
    private GameTypes.Lane mLane;
    private float mHealth = 100.0f;
    private float mDamage = 5.0f;

    public HealthBar mHealthBar;

    void Start()
    {
        mHealthBar.setMaxHealth(mHealth);
    }

     void Update()
    {
        
    }
    public GameTypes.Lane lane
    {
        get { return mLane; }
        set
        {
            mLane = value;
        }
    }

    public void TakeDamage(float _damage)
    {
        mHealth -= _damage;
        mHealthBar.setHealth(mHealth);
        if(mHealth <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    
}
