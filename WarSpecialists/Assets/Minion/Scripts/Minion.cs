using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
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
        if(mHealth <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private GameTypes.Lane mLane;
    private float mHealth = 100.0f;
    private float mDamage = 5.0f;
}
