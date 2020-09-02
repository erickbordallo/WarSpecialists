﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    public float speed = 70f;
    public int damage = 50;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            //Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFram = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFram)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFram, Space.World);
    }


    void HitTarget()
    {
        Damage(target);
        // Destroy(target.gameObject);
        Destroy(gameObject);
    }

    void Damage(Transform player)
    {
        TargetHealth p = player.GetComponent<TargetHealth>();
        if (p != null)
        {
            p.TakeDamage(damage);
        }

    }

}
