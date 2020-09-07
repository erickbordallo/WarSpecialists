﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerMovement))]
public class HeroCombat : MonoBehaviour
{
    public enum HeroAttackType { Melee, Ranged }
    public HeroAttackType heroAttackType;

    public GameObject targetedEnemy;
    public float rotateSpeedForAttack;

    private PlayerMovement movement;

    public bool basicAtkIdle = true;
    public bool isHeroAlive;
    public bool performMeleeAttack = true;

    public bool IsAttacking { get; set; }

    private float attackDelayTime;
    private float attackTimer = 0f;
    private float attackRange, attackDamage;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        IsAttacking = false;
        attackDelayTime = gameObject.GetComponent<PlayerBase>().AttackSpeed;
        attackTimer = attackDelayTime;
        attackRange = gameObject.GetComponent<PlayerBase>().AttackRange;
        attackDamage = gameObject.GetComponent<PlayerBase>().Attack;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (targetedEnemy != null)
        {
            if ((Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position)) > attackRange)
            {
                movement.IsMoving = true;
                movement.Agent.SetDestination(targetedEnemy.transform.position);
                movement.Agent.stoppingDistance = attackRange;

                Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y,
                    ref movement._rotationVelocity,
                    rotateSpeedForAttack * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
            else
            {
                if (heroAttackType == HeroAttackType.Melee)
                {
                    if (performMeleeAttack)
                    {
                        IsAttacking = true;
                        
                        if (attackTimer < 0f)
                        {
                        
                            targetedEnemy.GetComponent<Minion>().TakeDamage(attackDamage);
                            Debug.Log("Attack the minion");
                            attackTimer = attackDelayTime;
                        }
                    }
                }
            }
        }
    }
}
