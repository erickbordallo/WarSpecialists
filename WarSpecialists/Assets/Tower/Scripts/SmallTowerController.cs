using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTowerController : MonoBehaviour
{

    [SerializeField]
    public Transform m_AttackPos;

    public bool m_IsFriend;

    public int Target;

    public GameObject enemys;

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Emeny")
        {
            enemys.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        enemys.Remove(other.gameObject);
    }

    private void Attack()
    {
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(BulletPrefebs, ShootPosition.position, transform.rotation);
            bullet.GetComponent<Bullet>().SetTarget(emenys[0]);
        }

    }
}
