using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
   [SerializeField] private List<PlayerHealth> playerList;
    private Transform target;

    [Header("Tower Health")]
    public float startHealth = 100;
    private float health;
    public GameObject deathEffect;
    public Image healthBar;

    [Header("Attributes")]

    public float range = 4.0f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    public string playerTag = "Player";

    public Transform partToRotate;
    public float turnSpeed = 10.0f;

    public GameObject bulletPrefab;
    public Transform firePoint;


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        health = startHealth;
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        if(health <0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

    void UpdateTarget()
    {
        if(target == null)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestPlayer = null;

            foreach (GameObject player in players)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                if (distanceToPlayer < shortestDistance)
                {
                    shortestDistance = distanceToPlayer;
                    nearestPlayer = player;
                }
            }

            if (nearestPlayer != null && shortestDistance <= range)
            {
                target = nearestPlayer.transform;
            }

            else
            {
                target = null;

            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerHealth>();
        if (player)
        {
            playerList.Add(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerHealth>();
        if (player)
        {
            playerList.Remove(player);
        }
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }
        //rotation the tower
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        
        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
