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

    public GameTypes.Team team
    {
        get { return mTeam; }
        set
        {
            mTeam = value;
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
    private GameTypes.Team mTeam;
    public float mHealth = 300.0f;
    private float mDamage = 5.0f;
}
