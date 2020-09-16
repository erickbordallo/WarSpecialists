using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTarget : MonoBehaviour
{
    public GameObject selectedHero;
    public bool heroPlayer;
    RaycastHit hit;
    private HeroCombat heroCombat;

    void Start()
    {
        selectedHero = GameObject.FindGameObjectWithTag("Player");
        heroCombat = gameObject.GetComponent<HeroCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if (hit.collider.GetComponent<Targetable>() != null)
                {
                    if (System.Enum.IsDefined(typeof(Targetable.EnemyType), hit.collider.gameObject.GetComponent<Targetable>().enemyType))
                    {
                        heroCombat.targetedEnemy = hit.collider.gameObject;
                    }
                }
                else
                {
                    heroCombat.targetedEnemy = null;
                    heroCombat.IsAttacking = false;
                }
            }
        }
    }
}
