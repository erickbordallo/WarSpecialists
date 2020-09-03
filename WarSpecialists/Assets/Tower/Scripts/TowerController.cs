using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    [SerializeField]
    private List<TargetHealth> playerList;
    private Transform target;

    public float range = 4.0f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public string playerTag = "Player";

    public Transform partToRotate;
    public float turnSpeed = 10.0f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Somebody get into the attack range of tower");
        var player = other.GetComponent<TargetHealth>();
        if (player)
        {
            playerList.Add(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<TargetHealth>();
        if (player)
        {
            playerList.Remove(player);
        }
    }

    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletControl bullet = bulletGo.GetComponent<BulletControl>();

        if (bullet != null)
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
